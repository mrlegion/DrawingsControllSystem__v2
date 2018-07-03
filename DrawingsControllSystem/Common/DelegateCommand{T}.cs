using System;

namespace DrawingsControllSystem.Common
{
    public class DelegateCommand<T> : DelegateCommandBase
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public DelegateCommand(Action<T> execute) : this(execute, arg => true) { }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null || canExecute == null)
                throw new ArgumentNullException(nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(T parametr) => execute(parametr);

        public bool CanExecute(T parametr) => canExecute(parametr);

        protected override void Execute(object parameter) => execute((T)parameter);

        protected override bool CanExecute(object parametr) => CanExecute((T)parametr);
    }

}