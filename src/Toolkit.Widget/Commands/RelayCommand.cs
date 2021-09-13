using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Toolkit.Widget.Commands
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _CanExecute = canExecute;
        }

        private readonly Action _Execute;
        private readonly Func<bool> _CanExecute;

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null || _CanExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _Execute();
        }

        #endregion ICommand Members
    }

    internal class RelayCommand<T> : ICommand where T : class
    {
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _CanExecute = canExecute;
        }

        private readonly Action<T> _Execute;
        private readonly Predicate<T> _CanExecute;

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null || _CanExecute(parameter as T);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _Execute(parameter as T);
        }

        #endregion ICommand Members
    }
}