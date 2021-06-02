using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Initial")]
    public class CreateTexts : ImportBlock<TextSegment[]>
    {
        [Category("Enter")] public string List_Texts { set; get; }

        private List<string[]> textArray { set; get; }

        public override bool CanExecute()
        {
            var point = List_Texts
                .Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
            textArray = point.Select(a => a.Split(',')).ToList();
            return textArray.All(a => a.Length == 3);
        }

        public override void Execute()
        {
            OutPut = textArray
                .Select(a => new TextSegment(a[0], double.Parse(a[1]), double.Parse(a[1])))
                .ToArray();
        }
    }


    public struct TextSegment
    {
        public string Text { set; get; }
        public Point Point { set; get; }

        public TextSegment(string text, double x, double y)
        {
            Text = text;
            Point = new Point(x, y);
        }
    }
}
