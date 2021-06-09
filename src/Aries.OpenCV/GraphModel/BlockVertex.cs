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

            var outMatDict = TypeDescriptor.GetProperties(GetType())
                .OfType<PropertyDescriptor>()
                .Where(a => a.Category == "OUTPUT")
                .Select(a => new KeyValuePair<string, Mat>(a.Name, GetPropertyAsMat(a.Name) as Mat))
                .Where(a => a.Value != null);

            return outMatDict
                .Select(mat => MatRecord(workDirectory, mat.Key, mat.Value))
                .Where(a => a != null)
                .ToList();
        }

        private MatRecord MatRecord(string workDirectory,string propertyName, Mat mat)
        {
            var fileName = Path.Combine(workDirectory, $"{Name}_{propertyName}_{ID}_{DateTime.Now:yy_MM_dd_HH_mm_ss}.jpg");
            var res = mat.ImWrite(fileName);
            return res
                ? new MatRecord
                {
                    FileName = fileName,
                    ParentId = ID,
                    ParentName = Name,
                    PropertyName = propertyName,
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

        public object GetPropertyAsMat(string proName)
        {
            var propertyInfo = GetType().GetProperty(proName);
            if (propertyInfo == null)
                return null;

            var type = propertyInfo.PropertyType;
            var valueObj = propertyInfo.GetValue(this);
            if (type == typeof(Mat))
            {
                return valueObj as Mat;
            }

            if (type == typeof(InputArray))
            {
                return (valueObj as InputArray)?.GetMat();
            }

            if (type == typeof(OutputArray))
            {
                return (valueObj as OutputArray)?.GetMat();
            }
            return null;
        }

        public void SetPropertyAsMat(string proName, object value)
        {
            
            var mat = value as Mat;
            if (mat == null)
                return;

            var propertyInfo = GetType().GetProperty(proName);
            if (propertyInfo == null)
                return;

            var type = propertyInfo.PropertyType;
            if (type == typeof(Mat))
            {
                propertyInfo.SetValue(this, mat);
            }
            else if (type == typeof(InputArray))
            {
                propertyInfo.SetValue(this, InputArray.Create(mat));
            }
            else if (type == typeof(OutputArray))
            {
                propertyInfo.SetValue(this, OutputArray.Create(mat));
            }

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
