using System.Windows.Controls;
using DrawingsControllSystem.Common;
using DrawingsControllSystem.Views;
using Unity;

namespace DrawingsControllSystem.ViewModels
{
    public class SelectViewModel : BindableBase
    {
        private readonly IUnityContainer container;

        public SelectViewModel(IUnityContainer container)
        {
            this.container = container;

            OpenWatchCommand = new DelegateCommand(() =>
            {
                Page page = this.container.Resolve<WatcherView>();
                page.DataContext = this.container.Resolve<WatcherViewModel>();
                this.container.Resolve<ShellViewModel>().Current = page;
            });

            OpenSearchCommand = new DelegateCommand(() =>
            {
                Page page = this.container.Resolve<SearcherView>();
                page.DataContext = this.container.Resolve<SearcherViewModel>();
                this.container.Resolve<ShellViewModel>().Current = page;
            });
        }

        public DelegateCommand OpenWatchCommand { get; }
        public DelegateCommand OpenSearchCommand { get; }
    }
}