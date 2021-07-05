using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GalaSoft.MvvmLight.Command;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMats
{
    public class AriesExpander : VertexMats
    {

        private Mat[] _matsIn;

        [Category("DATAIN")]
        public Mat[] MatsIn
        {
            get { return _matsIn; }
            set
            {
                _matsIn = value;
                RaisePropertyChanged(() => MatsIn);
            }
        }

        [Category("ARGUMENT")]
        public string GraphCVAriesFile { set; get; }


        [Category("COMMAND")]
        public RelayCommand SelectAriesCommand
        {
            get { return new RelayCommand(SelectAriesCommand_Execute); }
        }

        private void SelectAriesCommand_Execute()
        {
            
        }


        public override bool CanExecute()
        {
            return MatsIn != null && MatsIn.Length >= 1;
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
