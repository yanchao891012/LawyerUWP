using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUWP.ViewModel
{
    class VMLocator
    {
        private static MainViewModel _mainVM;

        internal static MainViewModel MainVM
        {
            get
            {
                return _mainVM ?? (_mainVM = new MainViewModel());
            }
        }

        private static SearchLawyerViewModel _searchLawyerVM;

        internal static SearchLawyerViewModel SearchLawyerVM
        {
            get
            {
                return _searchLawyerVM ?? (_searchLawyerVM = new SearchLawyerViewModel());
            }
        }

        private static HomeViewModel _homeVM;

        internal static HomeViewModel HomeVM
        {
            get
            {
                return _homeVM ?? (_homeVM = new HomeViewModel());
            }
        }

        private static SearchArgsViewModel _searchArgsVM;

        internal static SearchArgsViewModel SearchArgsVM
        {
            get
            {
                return _searchArgsVM ?? (_searchArgsVM = new SearchArgsViewModel());
            }
        }

        public static void IsPaneOpen()
        {
            Messenger.Default.Send<object>(null, "IsPaneOpen");
        }

        public static void Visible()
        {
            HomeVM.BtnVisibility = Windows.UI.Xaml.Visibility.Visible;
            SearchLawyerVM.BtnVisibility = Windows.UI.Xaml.Visibility.Visible;
            SearchArgsVM.BtnVisibility = Windows.UI.Xaml.Visibility.Visible;
        }

        public static void Collapsed()
        {
            HomeVM.BtnVisibility = Windows.UI.Xaml.Visibility.Collapsed;
            SearchLawyerVM.BtnVisibility = Windows.UI.Xaml.Visibility.Collapsed;
            SearchArgsVM.BtnVisibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
