using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DrawingsControllSystem.Common
{
    public class Navigation
    {
        private static volatile Navigation instance;
        private static readonly object SyncRoot = new Object();
        private NavigationService service;

        private Navigation () { }

        private static Navigation Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                            instance = new Navigation();
                    }
                }

                return instance;
            }
        }

        public static NavigationService Service
        {
            get { return Instance.service; }
            set
            {
                if (Instance.service != null)
                    Instance.service.Navigated -= Instance.NavigatedeHandler;

                Instance.service = value;
                Instance.service.Navigated += Instance.NavigatedeHandler;
            }
        }

        private void NavigatedeHandler(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;
            if (page == null) { return; }

            page.DataContext = e.ExtraData;
        }

        public static void Navigate(Page page, object context)
        {
            if (Instance.service == null || page == null) { return; }

            Instance.service.Navigate(page, context);
        }

        public static void Navigate(Page page) { Navigate(page, null); }
    }
}