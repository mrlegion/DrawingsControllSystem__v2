using System;
using System.Windows.Input;

namespace DrawingsControllSystem.Common
{
    public abstract class DelegateCommandBase : ICommand
    {
        protected abstract void Execute(object parameter);
        protected abstract bool CanExecute(object parametr);

        public virtual event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        bool ICommand.CanExecute(object parametr)
        {
            return CanExecute(parametr);
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }
    }

}