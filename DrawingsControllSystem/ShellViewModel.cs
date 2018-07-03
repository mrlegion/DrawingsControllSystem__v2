using System.Windows.Controls;
using DrawingsControllSystem.Common;
using DrawingsControllSystem.ViewModels;
using DrawingsControllSystem.Views;
using Unity;

namespace DrawingsControllSystem
{
    public class ShellViewModel : BindableBase
    {
        private string title = "Drawings Controll System";
        private readonly IUnityContainer container;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private Page current;

        public Page Current
        {
            get { return current; }
            set { SetProperty(ref current, value); }
        }

        public ShellViewModel(IUnityContainer container)
        {
            this.container = container;
            Current = this.container.Resolve<SelectView>();
            Current.DataContext = this.container.Resolve<SelectViewModel>();
        }
    }
}