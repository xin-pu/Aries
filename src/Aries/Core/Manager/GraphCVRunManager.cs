using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using Aries.Utility;
using GraphX.Common.Models;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Aries.Core
{
    public class GraphCVRunManager
    {
        private static readonly Lazy<GraphCVRunManager> lazy =
            new Lazy<GraphCVRunManager>(() => new GraphCVRunManager());

        public static GraphCVRunManager Instance
        {
            get { return lazy.Value; }
        }


        public AriesMain AriesMain { set; get; }

        public List<GraphSerializationData> GraphSerializationDatas { set; get; }

        public ICommand StepRunGraphCVCommand
        {
            get { return new RelayCommand(StepRunGraphCVCommand_Execute); }
        }

        public ICommand RunGraphCVCommand
        {
            get { return new RelayCommand(RunGraphCVCommand_Execute); }
        }

        public ICommand StopGraphCVCommand
        {
            get { return new RelayCommand(StopGraphCVCommand_Execute); }
        }


        private void StepRunGraphCVCommand_Execute()
        {
           
        }

        private void RunGraphCVCommand_Execute()
        {
            try
            {
                RunGraphCV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void StopGraphCVCommand_Execute()
        {

        }

        public async void RunGraphCV()
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {






            });
        }

    }
}
