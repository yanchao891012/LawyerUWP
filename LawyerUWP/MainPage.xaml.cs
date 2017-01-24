using GalaSoft.MvvmLight.Messaging;
using LawyerUWP.View;
using LawyerUWP.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace LawyerUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ShowStatusBar();
            VM = VMLocator.MainVM;
            this.InitializeComponent();
            mainNavigationList.SelectedIndex = 1;
            Messenger.Default.Register<object>(this, "IsPaneOpen", new
                Action<object>(p =>
                {
                    mainSplitView.IsPaneOpen = !mainSplitView.IsPaneOpen;
                }));
            this.Unloaded += (s, e) => Messenger.Default.Unregister(this);
        }

        private MainViewModel _vM;

        internal MainViewModel VM
        {
            get
            {
                return _vM;
            }

            set
            {
                _vM = value;
            }
        }

        int _preSelectNavigation = -1;

        private async void mainNavigationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem tapped_item = mainNavigationList.SelectedItems[0] as ListBoxItem;

            if (tapped_item != null && tapped_item.Tag != null)
            {
                mainSplitView.IsPaneOpen = false;
                _preSelectNavigation = mainNavigationList.SelectedIndex;
                if (tapped_item.Tag.ToString().Equals("1"))
                {
                    mainFrame.Navigate(typeof(HomePage), new object[] { this.slaveFrame });
                }
                //查找律师
                if (tapped_item.Tag.ToString().Equals("2"))
                {
                    mainFrame.Navigate(typeof(SearchLawyerPage),new object[] { this.slaveFrame});
                }
                //查找辩词
                if (tapped_item.Tag.ToString().Equals("3"))
                {
                    mainFrame.Navigate(typeof(SearchArgsPage), new object[] { this.slaveFrame });
                }

                if (tapped_item.Tag.ToString().Equals("4"))
                {
                    AboutPage about = new AboutPage();
                    await about.ShowAsync();
                }
            }
        }
        /// <summary>
        /// 导航栏显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListBoxItem tapped_item = sender as ListBoxItem;
            if (tapped_item != null && tapped_item.Tag != null && tapped_item.Tag.ToString().Equals("0"))
            {
                mainSplitView.IsPaneOpen = !mainSplitView.IsPaneOpen;
            }
        }

        private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            VM.Narrow();
        }

        private async void ShowStatusBar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusbar = StatusBar.GetForCurrentView();
                await statusbar.ShowAsync();
                statusbar.BackgroundColor = Colors.White;
                statusbar.BackgroundOpacity = 1;
                statusbar.ForegroundColor = Colors.Black;
            }
        }
    }
}
