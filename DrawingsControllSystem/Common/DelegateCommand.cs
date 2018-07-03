using System;

namespace DrawingsControllSystem.Common
{
    public class DelegateCommand : DelegateCommandBase
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public DelegateCommand(Action execute) : this(execute, () => true) { }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null || canExecute == null)
                throw new ArgumentNullException(nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute() => execute();

        public bool CanExecute() => canExecute();

        protected override void Execute(object parameter) => Execute();

        protected override bool CanExecute(object parametr) => CanExecute();
    }

}