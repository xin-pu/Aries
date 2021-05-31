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
        public string CVCategory { set; get; }

        public string Icon { set; get; }

        [Category("INFO")]
        public string Name { set; get; }
        [Category("INFO")]
        public DateTime? StartTime { set; get; }
        [Category("INFO")]
        public DateTime? StopTime { set; get; }
        [Category("INFO")]
        public TimeSpan? TimeCost { set; get; }
        [Category("INFO")]
        public string ErrorMessage { set; get; }
        [Category("INFO")]
        public BlockStatus Status
        {
            set { UpdateProperty(ref _status, value); }
            get { return _status; }
        }


        public bool EnableSaveBlock { get; set; }
        public string SaveBlockName { set; get; }



        protected BlockVertex()
        {
            Initial();
        }

        private void Initial()
        {
            CVCategory = BlockHelper.GetCvCategory(GetType());
            Icon = BlockHelper.GetBlockICon(CVCategory);
        }

        public ICommand RunBlockCommand
        {
            get { return new RelayCommand(ExecuteCommand_Execute, ExecuteCommand_CanExecute); }
        }


        public bool ExecuteCommand_CanExecute()
        {
            return CanExecute();
        }

        public virtual void ExecuteCommand_Execute()
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
                TimeCost = StopTime - StartTime;
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
