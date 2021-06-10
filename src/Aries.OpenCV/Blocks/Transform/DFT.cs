﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Transform")]
    public class DFT : MatProcessingBlock
    {

        /// <summary>
        /// The number of channels in the destination image;
        /// if the parameter is 0, the number of the channels will be derived automatically
        /// from src and the code
        /// </summary>
        [Category("ARGUMENT")]
        public DftFlags DftFlag { set; get; } = DftFlags.None;

        /// <summary>
        /// ColorConversionCodes.BGR2HSV
        /// ColorConversionCodes.BGR2GRAY
        /// </summary>
        [Category("ARGUMENT")]
        public int NonzeroRows { set; get; } = 0;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Dft(InPutMat, OutPutMat, DftFlag, NonzeroRows);
        }
    }
}
