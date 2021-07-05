using GalaSoft.MvvmLight.Command;
using GraphX.Common.Models;
using OpenCvSharp;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Aries.OpenCV.GraphModel
{
    public abstract class VertexBasic : VertexBase
    {
        private BlockStatus _status = BlockStatus.ToRun;

        private string _workDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public string CVCategory { set; get; }
        public string Icon { set; get; }

        [Category("INFO")] public string Name { set; get; }


        [Category("INFO")] public TimeSpan? TimeCost { set; get; }

        [Category("INFO")] public string ErrorMessage { set; get; }



        [Category("INFO")]
        public string WorkDirectory
        {
            get { return _workDirectory; }
            set
            {
                _workDirectory = value;
                RaisePropertyChanged(() => WorkDirectory);
            }
        }

        [Category("INFO")]
        public BlockStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }


        protected VertexBasic()
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
            return CanCall();
        }

        public virtual void ExecuteCommand_Execute()
        {
            var startTime = DateTime.Now;
            try
            {

                Status = BlockStatus.Run;
                Call();
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

        }

        public virtual void Reload()
        {
            TimeCost = null;
            Status = BlockStatus.ToRun;
            var pros = BlockHelper.GetInOUT(GetType());
            pros.ForEach(pro => pro.SetValue(this, null));
        }


        public abstract bool CanCall();
        public abstract void Call();


        public virtual async Task<bool> CanCallAsync()
        {
            return await Task.Run(CanCall);
        }

        public virtual async void ExecuteAsync()
        {
            await Task.Run(Call);
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
    }
}
