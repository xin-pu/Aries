using System.Collections.Generic;
using System.Windows.Input;

namespace Aries.Core
{
    public class AriesManager
    {
        public ICommand GraphCVOpenCommand { set; get; }
        public ICommand GraphCVCloseCommand { set; get; }
        public ICommand GraphCVNewCommand { set; get; }
        
        
        public ICommand GraphCVSaveCommand { set; get; }
        public ICommand GraphCVSaveAsCommand { set; get; }


        public LogicCoreCV LogicCoreCv { set; get; }

        public AriesManager()
        {

        }



    }
}
