using System.Windows;
using System.Windows.Controls;
using DrawingsControllSystem.Common;
using DrawingsControllSystem.ViewModels;
using DrawingsControllSystem.Views;
using Unity;

namespace DrawingsControllSystem
{
    public class Bootstrapper
    {
        private readonly IUnityContainer container;

        public Bootstrapper()
        {
            container = new UnityContainer();
        }

        private void CreateShell()
        {
            container.RegisterSingleton<Shell>();
            container.RegisterSingleton<ShellViewModel>();
        }

        private void InitializeShell()
        {
            Shell shell = container.Resolve<Shell>();
            shell.DataContext = container.Resolve<ShellViewModel>();
            Application.Current.MainWindow = shell;
            Application.Current.MainWindow?.Show();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void RegisterPage()
        {
            // Выбор типа программы
            container.RegisterSingleton<SelectView>();
            container.RegisterSingleton<SelectViewModel>();

            // Тип отслеживания
            container.RegisterSingleton<WatcherView>();
            container.RegisterSingleton<WatcherViewModel>();

            // Тип поиска по выбранному пути
            container.RegisterSingleton<SearcherView>();
            container.RegisterSingleton<SearcherViewModel>();
        }

        public void Run()
        {
            RegisterPage();
            CreateShell();
            InitializeShell();

        }
    }
}