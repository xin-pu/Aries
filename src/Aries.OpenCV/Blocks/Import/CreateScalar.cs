using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Initial")]
    public class CreateScalar : ImportBlock<Scalar>
    {

        /// <summary>
        /// 
        /// </summary>
        [Category("ARGUMENT")]
        public double Val0 { set; get; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Category("ARGUMENT")]
        public double Val1 { set; get; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Category("ARGUMENT")]
        public double Val2 { set; get; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [Category("ARGUMENT")]
        public double Val3 { set; get; } = 0;


        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            TOut = new Scalar(Val0, Val1, Val2, Val3);
        }
    }
}
