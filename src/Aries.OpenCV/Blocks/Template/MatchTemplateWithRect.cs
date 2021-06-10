using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Template
{

    [Category("Template")]
    public class MatchTemplateWithRect : ProcessingBlock<Mat, Rect>
    {
        [Category("INPUT")] public Mat Template { set; get; }

        [Category("INPUT")] public Mat Mask { set; get; }

        /// <summary>
        /// The number of channels in the destination image;
        /// if the parameter is 0, the number of the channels will be derived automatically
        /// from src and the code
        /// </summary>
        [Category("ARGUMENT")]
        public TemplateMatchModes TemplateMatchMode { set; get; } = TemplateMatchModes.CCoeff;

        [Category("ARGUMENT")] public bool EnableMask { set; get; } = false;

        public override void Reload()
        {
            InPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != null &&
                   (!EnableMask || Mask != null);
        }

        public override void Execute()
        {
            var outMat = new Mat();
            Cv2.MatchTemplate(InPutMat, Template, outMat, TemplateMatchMode, Mask);
            Point pointMin, pointMax;
            Cv2.MinMaxLoc(outMat, out pointMin, out pointMax);

            var shape = Template.Size();
            var topLeft =
                TemplateMatchMode == TemplateMatchModes.SqDiff ||
                TemplateMatchMode == TemplateMatchModes.SqDiffNormed
                    ? pointMin
                    : pointMax;

            OutPutMat = new Rect(topLeft, shape);
        }
    }
}
