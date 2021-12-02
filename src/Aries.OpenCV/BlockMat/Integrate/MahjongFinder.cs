using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Aries.OpenCV.Utils;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat.Integrate
{
    internal class MahjongFinder : MatIntegrate<List<RotatedRect>>
    {
        private readonly Size MahSize = new(260, 350);
        [Category("ARGUMENT")] public double Ratio { set; get; } = 1.3365;

        [Category("ARGUMENT")] public int AreaThreshold { set; get; } = 90000;

        [Category("ARGUMENT")] public bool IsContainBack { set; get; } = false;

        [Category("ARGUMENT")] public double Threshold { set; get; } = 210;

        [Category("ARGUMENT")] public string TrayPattern { set; get; }

        [Category("COMMAND")] public RelayCommand SelectAriesCommand => new(SelectPatternCommand_Execute);

        private void SelectPatternCommand_Execute()
        {
            var openFileDailog = new OpenFileDialog
            {
                Title = $"{ID}_{Name}",
                Filter = "PNG文件|*.png"
            };
            openFileDailog.ShowDialog();
            TrayPattern = openFileDailog.FileName;
        }


        public override List<RotatedRect> Process(Mat matin)
        {
            var roi_1 = MatIn.Clone();

            roi_1 = PretreatmentForFront(roi_1);
            var front = FindRect(roi_1);

            if (IsContainBack)
            {
                var roi_2 = MatIn.Clone();
                front.ForEach(a => { fullRotatedRect(roi_2, a, new Scalar(0), 3); });

                roi_2 = PretreatmentForBack(roi_2);

                var back = FindRect(roi_2);

                var all = front.Concat(back).ToList();

                return all;
            }

            return front;
        }

        public override Mat Draw(Mat mat, List<RotatedRect> result)
        {
            result.ToList().ForEach(a => { DrawRotatedRect(mat, a, PenColor); });
            return mat;
        }


        /// <summary>
        ///     寻找正面朝上麻将的预处理
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        private Mat PretreatmentForFront(Mat mat)
        {
            if (File.Exists(TrayPattern))
            {
                var pattern = Cv2.ImRead(TrayPattern, ImreadModes.Grayscale);
                mat = RemoveLight(mat, pattern);
            }

            Cv2.Threshold(mat, mat, Threshold, 255, ThresholdTypes.Binary);

            var element = Cv2.GetStructuringElement(MorphShapes.Rect,
                new Size(5, 5),
                new Point(-1, -1));

            Cv2.MorphologyEx(mat, mat, MorphTypes.Open, element, new Point(-1, -1));

            return mat;
        }


        /// <summary>
        ///     寻找背面朝上麻将的预处理
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        private Mat PretreatmentForBack(Mat mat)
        {
            /// 60为经验值，用于分割背部麻将
            Cv2.Threshold(mat, mat, 40, 255, ThresholdTypes.Triangle);

            /// 开运算······················································································统计区域数
            var element1 = Cv2.GetStructuringElement(MorphShapes.Rect,
                new Size(9, 9),
                new Point(-1, -1));
            Cv2.MorphologyEx(mat, mat, MorphTypes.Open, element1, new Point(-1, -1), 2);

            return mat;
        }

        private List<RotatedRect> FindRect(Mat mat)
        {
            var Hierarchy = new Mat();
            Cv2.FindContours(mat, out var filtercons, Hierarchy, RetrievalModes.External,
                ContourApproximationModes.ApproxSimple);

            filtercons = filtercons
                .Where(a =>
                {
                    var rotatedRect = Cv2.MinAreaRect(a);
                    var rect_area = rotatedRect.Size.Width * rotatedRect.Size.Height;
                    return rect_area >= MahSize.Height * MahSize.Width * 0.36;
                }).ToArray();

            var all = filtercons
                .Select(a => Cv2.MinAreaRect(a))
                .ToList();
            var suitRes = all.Where(a =>
            {
                var area = a.Size.Width * a.Size.Height;
                var areaRes = area > AreaThreshold * 0.8 && area < AreaThreshold * 1.2;

                var ratio = CVMath.GetRatio(a);
                var rationRes = ratio > Ratio * 0.8 && ratio < Ratio * 1.2;
                return areaRes && rationRes;
            }).ToList();

            if (all.Any(a =>
            {
                var area = a.Size.Width * a.Size.Height;
                return area >= AreaThreshold * 1.2;
            }))
                throw new Exception("Find Mahjong Meet Area > 1.2 Mahjong");

            return suitRes;
        }


        public Mat RemoveLight(Mat mat, Mat pattern)
        {
            Cv2.BitwiseNot(pattern, pattern);
            Cv2.BitwiseNot(mat, mat);
            mat.ConvertTo(mat, MatType.CV_32F);
            pattern.ConvertTo(pattern, MatType.CV_32F);
            var res = new Mat();
            Cv2.Divide(mat, pattern, res);
            mat = (255 * (1 - res)).ToMat();
            mat.ConvertTo(mat, MatType.CV_8U);
            mat.SaveImage("removeLight.png");
            return mat;
        }


        #region fullRotatedRect

        private void fullRotatedRect(Mat mat, RotatedRect rotatedRect, Scalar color, int thickness)
        {
            var points = rotatedRect.Points();

            // 绘制外框
            Enumerable.Range(0, 4).ToList().ForEach(i =>
            {
                Cv2.Line(mat, (Point) points[i], (Point) points[(i + 1) % 4], color);
            });


            /*填充内部*/
            var center1 = new List<Point>();
            var center2 = new List<Point>();
            //再利用四个顶点找出其中平行两边的所有点，对相应的两个点进行连接。
            findAllPoint(points[0].ToPoint(), points[1].ToPoint(), center1); /*找出两点间直线上的所有点*/
            findAllPoint(points[3].ToPoint(), points[2].ToPoint(), center2);

            center1.Zip(center2, (a, b) => (a, b))
                .ToList()
                .ForEach(pair => { Cv2.Line(mat, pair.a, pair.b, color, thickness); });
        }


        private void findAllPoint(Point start, Point end, List<Point> save)
        {
            if (Math.Abs(start.X - end.X) <= 1 && Math.Abs(start.Y - end.Y) <= 1)
            {
                save.Add(start);
                return; /*点重复时返回*/
            }

            var point_center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            save.Add(point_center); /*储存中点*/
            findAllPoint(start, point_center, save); /*递归*/
            findAllPoint(point_center, end, save);
        }

        #endregion
    }
}