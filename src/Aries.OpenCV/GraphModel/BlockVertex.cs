using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.OpenCV.Core;
using Aries.Utility;
using GraphX.Common.Models;
using OpenCvSharp;

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


        protected BlockVertex()
        {
            Initial();
        }

        private void Initial()
        {
            Name = GetType().Name;
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


        public virtual bool EnableSaveBlock { get; set; } = true;

        public virtual List<MatRecord> SaveBlock(string workDirectory)
        {
            if (Status != BlockStatus.Complete || !EnableSaveBlock)
                return new List<MatRecord>(0);

            if (workDirectory == string.Empty || !Directory.Exists(workDirectory))
                return new List<MatRecord>(0);

            var outMatPro = TypeDescriptor.GetProperties(GetType())
                .OfType<PropertyDescriptor>()
                .Where(a => a.PropertyType == typeof(Mat) && a.Category == "OUT_MAT");

            var outMat = outMatPro.Select(a => a.GetValue(this) as Mat)
                .Where(a => a != null);

            return outMat
                .Select(mat => MatRecord(workDirectory, mat))
                .Where(a => a != null)
                .ToList();
        }

        private MatRecord MatRecord(string workDirectory, Mat mat)
        {
            var fileName = Path.Combine(workDirectory, $"{Name}_{ID}_{DateTime.Now:yy_MM_dd_HH_mm_ss}.jpg");
            var res = mat.ImWrite(fileName);
            return res
                ? new MatRecord
                {
                    FileName = fileName,
                    ParentId = ID,
                    ParentName = Name,
                    UpDateTime = DateTime.Now
                }
                : null;
        }


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
}
