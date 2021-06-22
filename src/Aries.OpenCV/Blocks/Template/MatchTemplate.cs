using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Template
{

    [Category("Template")]
    public class MatchTemplate : MatProcessingBlock
    {
        [Category("DATAIN")] public Mat Template { set; get; }

        [Category("DATAIN")] public Mat Mask { set; get; }

        /// <summary>
        /// The number of channels in the destination image;
        /// if the parameter is 0, the number of the channels will be derived automatically
        /// from src and the code
        /// </summary>
        [Category("ARGUMENT")]
        public TemplateMatchModes TemplateMatchMode { set; get; } = TemplateMatchModes.CCoeff;

        [Category("CHOICE")] public bool EnableMask { set; get; } = false;

        public override bool CanExecute()
        {
            return MatIn != null &&
                   (!EnableMask || Mask != null);
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.MatchTemplate(MatIn, Template, MatOut, TemplateMatchMode, Mask);
        }
    }
}
