using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Aries.OpenCV.Core;
using GalaSoft.MvvmLight.Command;
using GraphX.Common.Models;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public abstract class BlockVertex : VertexBase, INotifyPropertyChanged
    {
        private BlockStatus _status = BlockStatus.ToRun;
        private bool _enableSaveBlock = true;
        private string _outImage = string.Empty;
        private string _workDire = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public string CVCategory { set; get; }
        public string Icon { set; get; }

        [Category("INFO")] public string Name { set; get; }


        [Category("INFO")] public TimeSpan? TimeCost { set; get; }

        [Category("INFO")] public string ErrorMessage { set; get; }

        [Category("INFO")]
        public string OutImage
        {
            set { UpdateProperty(ref _outImage, value); }
            get { return _outImage; }
        }

        [Category("INFO")]
        private string WorkDire
        {
            set { UpdateProperty(ref _workDire, value); }
            get { return _workDire; }
        }

        [Category("INFO")]
        public BlockStatus Status
        {
            set { UpdateProperty(ref _status, value); }
            get { return _status; }
        }

        [Category("CHOICE")]
        public bool EnableSaveBlock
        {
            set { UpdateProperty(ref _enableSaveBlock, value); }
            get { return _enableSaveBlock; }
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

        [Category("COMMAND")]
        public RelayCommand RunBlockCommand
        {
            get { return new RelayCommand(ExecuteCommand_Execute, ExecuteCommand_CanExecute); }
        }


        public bool ExecuteCommand_CanExecute()
        {
            return CanExecute();
        }

        public virtual void ExecuteCommand_Execute()
        {
            var startTime = DateTime.Now;
            try
            {

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
                var stopTime = DateTime.Now;
                TimeCost = stopTime - startTime;
            }

            if (EnableSaveBlock)
                SaveMatOut();
        }

        public virtual void Reload()
        {
            TimeCost = null;
            Status = BlockStatus.ToRun;
        }

        public abstract bool CanExecute();
        public abstract void Execute();


        public virtual async Task<bool> CanExecuteAsync()
        {
            return await Task.Run(CanExecute);
        }

        public virtual async void ExecuteAsync()
        {
            await Task.Run(Execute);
        }



        public virtual void SaveMatOut()
        {
            try
            {
                if (Status != BlockStatus.Complete || !EnableSaveBlock)
                    return;

                if (WorkDire == string.Empty || !Directory.Exists(WorkDire))
                    return;

                var outMatDict = TypeDescriptor.GetProperties(GetType())
                    .OfType<PropertyDescriptor>()
                    .FirstOrDefault(a => a.Category == "DATAOUT" && a.Name == "MatOut");

                var mat = GetPropertyAsMat(outMatDict?.Name) as Mat;

                OutImage = Path.Combine(WorkDire, $"{Name}_{ID}_{DateTime.Now:yy_MM_dd_HH_mm_ss}.jpg");
                mat?.ImWrite(OutImage);
            }
            catch (Exception ex)
            {
                // ignored
            }
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
