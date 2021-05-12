using System;
using System.Windows.Input;

namespace Aries.Utility
{
    public class RelayCommand : ICommand
    {
        #region

        public bool ExecuteWithPara { set; get; }

        private readonly Func<bool> _canExecute;
        private readonly Action _execute;
        private readonly Action<object> _executeWithPara;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        public RelayCommand(Action execute) : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            ExecuteWithPara = false;
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            ExecuteWithPara = true;
            _executeWithPara = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() != false;
        }

        public void Execute(object parameter)
        {
            if (ExecuteWithPara)
            {
                _executeWithPara?.Invoke(parameter);
            }
            else
            {
                _execute?.Invoke();
            }
        }

    }
}
