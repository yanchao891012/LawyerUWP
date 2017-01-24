using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace LawyerUWP.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ZMainFrame = 0;
            ZSlaveFrame = 1;
            MasterGrid = GridLength.Auto;
            DetailGrid = new GridLength(1, GridUnitType.Star);
            GridInt = 1;
            if (Window.Current.Bounds.Width < 720)
            {
                PaneInt = 0;
                VMLocator.Visible();
            }                
            else
            {
                PaneInt = 44;
                VMLocator.Collapsed();
            }
        }

        private GridLength _masterGrid;

        public GridLength MasterGrid
        {
            get
            {
                return _masterGrid;
            }

            set
            {
                _masterGrid = value;
                RaisePropertyChanged("MasterGrid");
            }
        }


        private GridLength _detailGrid;

        public GridLength DetailGrid
        {
            get
            {
                return _detailGrid;
            }

            set
            {
                _detailGrid = value;
                RaisePropertyChanged("DetailGrid");
            }
        }

        private int _paneInt;

        public int PaneInt
        {
            get
            {
                return _paneInt;
            }

            set
            {
                _paneInt = value;
                RaisePropertyChanged("PaneInt");
            }
        }

        private int _zMainFrame;

        public int ZMainFrame
        {
            get
            {
                return _zMainFrame;
            }

            set
            {
                _zMainFrame = value;
                RaisePropertyChanged("ZMainFrame");
            }
        }

        private int _zSlaveFrame;

        public int ZSlaveFrame
        {
            get
            {
                return _zSlaveFrame;
            }

            set
            {
                _zSlaveFrame = value;
                RaisePropertyChanged("ZSlaveFrame");
            }
        }

        private int _gridInt;

        public int GridInt
        {
            get
            {
                return _gridInt;
            }

            set
            {
                _gridInt = value;
                RaisePropertyChanged("GridInt");
            }
        }

        private bool _hasFrame;

        public bool HasFrame
        {
            get
            {
                return _hasFrame;
            }

            set
            {
                _hasFrame = value;
                RaisePropertyChanged("HasFrame");
            }
        }
        public void Narrow()
        {
            if (Window.Current.Bounds.Width < 720)
            {
                MasterGrid = new GridLength(1, GridUnitType.Star);
                DetailGrid = GridLength.Auto;
                GridInt = 0;
                PaneInt = 0;
                VMLocator.Visible();
                if (HasFrame)
                {
                    ZMainFrame = 0;
                }
                else
                {
                    ZMainFrame = 2;
                }
            }
            else
            {
                MasterGrid = GridLength.Auto;
                DetailGrid = new GridLength(1, GridUnitType.Star);
                GridInt = 1;
                PaneInt = 44;
                VMLocator.Collapsed();
            }
        }
    }
}
