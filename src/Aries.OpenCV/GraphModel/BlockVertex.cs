using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.OpenCV.Core;
using Aries.Utility;
using GraphX.Common.Models;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public abstract class BlockVertex : VertexBase, INotifyPropertyChanged
    {
        public BlockStatus _status;

        public BlockType BlockType { set; get; }

        public string Name { set; get; }
        public string InstName { set; get; }

        public string Icon { set; get; }



        public DateTime? StartTime { set; get; }
        public DateTime? StopTime { set; get; }
        public TimeSpan? TimeCost { set; get; }
        public string ErrorMessage { set; get; }

        public BlockStatus Status
        {
            set { UpdateProperty(ref _status, value); }
            get { return _status; }
        }

        protected BlockVertex()
        {
            Icon = getICon();
        }

        private string getICon()
        {
            return BlockHelper.GetBlockICon(GetType());
        }

        public ICommand RunBlockCommand
        {
            get { return new RelayCommand(ExecuteCommand_Execute, ExecuteCommand_CanExecute); }
        }

        private bool ExecuteCommand_CanExecute()
        {
            return CanExecute();
        }

        private void ExecuteCommand_Execute()
        {
            try
            {
                StartTime = DateTime.Now;
                Status = BlockStatus.Run;
                Execute();
                Status = BlockStatus.Complete;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Status = BlockStatus.Exception;
            }
            finally
            {
                StopTime = DateTime.Now;
                TimeCost = StartTime - StopTime;
            }
        }

        public abstract void Reload();
        public abstract bool CanExecute();
        public abstract void Execute();


        public object GetProperty(string proName)
        {
            var propertyInfo = GetType().GetProperty(proName);
            return propertyInfo?.GetValue(this);
        }

        public void SetProperty(string proName, object value)
        {
            var propertyInfo = GetType().GetProperty(proName);
            propertyInfo?.SetValue(this, value);
        }


        #region

        internal void UpdateProperty<T>(ref T properValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(properValue, newValue))
            {
                return;
            }

            properValue = newValue;

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public enum BlockStatus
    {
        ToRun,
        Run,
        Complete,
        Exception
    }

}
