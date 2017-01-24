using LawyerUWP.Constant;
using LawyerUWP.Http;
using LawyerUWP.Model;
using LawyerUWP.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace LawyerUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchLawyerPage : Page
    {
        Frame slaveFrame;
        private MainViewModel _mainVM = VMLocator.MainVM;
        internal MainViewModel MainVM
        {
            get
            {
                return _mainVM;
            }

            set
            {
                _mainVM = value;
            }
        }

        private SearchLawyerViewModel _searchLawyerVM;

        internal SearchLawyerViewModel SearchLawyerVM
        {
            get
            {
                return _searchLawyerVM;
            }

            set
            {
                _searchLawyerVM = value;
            }
        }

        public SearchLawyerPage()
        {
            //SearchLawyerVM = VMLocator.SearchLawyerVM;
            this.InitializeComponent();
            this.DataContext = VMLocator.SearchLawyerVM;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

            MainVM.HasFrame = true;
            MainVM.Narrow();

            slaveFrame.Navigate(typeof(LawyerInfo), new object[] { e.ClickedItem });
            slaveFrame.Navigated += SlaveFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += SearchLawyerPage_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = slaveFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void SearchLawyerPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (slaveFrame == null)
            {
                return;
            }
            if (slaveFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                slaveFrame.GoBack();
            }

            MainVM.HasFrame = false;
            MainVM.Narrow();
        }

        private void SlaveFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = slaveFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (App.PageState != null && App.PageState["SearchLawyerPage_PivotSelectIndex"] != null)
                {
                    pivotLawyers.SelectedIndex = (int)App.PageState["SearchLawyerPage_PivotSelectIndex"];
                }
            }

            object[] parameters = e.Parameter as object[];
            if (parameters != null)
            {
                if (parameters.Length == 1 && (parameters[0] as Frame) != null)
                {
                    slaveFrame = parameters[0] as Frame;
                }
            }
            slaveFrame.Navigate(typeof(BlankPage));
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = slaveFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        /// <summary>
        /// 页面离开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (App.PageState == null)
            {
                App.PageState = new Dictionary<string, object>();
            }
            App.PageState.Remove("SearchLawyerPage_PivotSelectIndex");
            App.PageState.Add("SearchLawyerPage_PivotSelectIndex", pivotLawyers.SelectedIndex);//保存当前pivot的选择项
        }

        private void tb_Click(object sender, RoutedEventArgs e)
        {
            VMLocator.IsPaneOpen();
        }
    }
}
