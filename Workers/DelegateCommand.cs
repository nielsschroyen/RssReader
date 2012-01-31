using System;
using System.Windows.Input;

namespace Reader.Workers
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public DelegateCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute.Invoke(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void InvokeCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        public DelegateCommand(Func<T, bool> canExecute, Action<T> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public DelegateCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute.Invoke((T) parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke((T) parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}