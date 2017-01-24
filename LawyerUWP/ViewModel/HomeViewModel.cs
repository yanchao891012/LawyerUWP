using GalaSoft.MvvmLight;
using LawyerUWP.Data;
using LawyerUWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml;

namespace LawyerUWP.ViewModel
{
    class HomeViewModel: ViewModelBase
    {
        /// <summary>
        /// 声明List
        /// </summary>
        private LawyerList _lawyerRandList;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HomeViewModel()
        {
            SetCitys();
            LawyerList = _lawyerRandList = new LawyerList(Constant.Method.LawyerByRand, "");
            _lawyerRandList.DataLoaded += _lawyerRandList_DataLoaded;
            _lawyerRandList.DataLoading += _lawyerRandList_DataLoading;
        }

        private void _lawyerRandList_DataLoading()
        {
            ProgressRingVisibility = Visibility.Visible;
        }

        private void _lawyerRandList_DataLoaded()
        {
            ProgressRingVisibility = Visibility.Collapsed;
        }

        #region 属性

        private ObservableCollection<Lawyer> _lawyerList = new ObservableCollection<Lawyer>();
        /// <summary>
        /// 律师列表
        /// </summary>
        public ObservableCollection<Lawyer> LawyerList
        {
            get { return _lawyerList; }
            set
            {
                _lawyerList = value;
                RaisePropertyChanged("LawyerList");
            }
        }

        private Visibility _progressRingVisibility = Visibility.Collapsed;
        /// <summary>
        /// 等待是否隐藏
        /// </summary>
        public Visibility ProgressRingVisibility
        {
            get
            {
                return _progressRingVisibility;
            }

            set
            {
                _progressRingVisibility = value;
                RaisePropertyChanged("ProgressRingVisibility");
            }
        }

        private Visibility _btnVisibility;
        /// <summary>
        /// 菜单按钮是否隐藏
        /// </summary>
        public Visibility BtnVisibility
        {
            get
            {
                return _btnVisibility;
            }

            set
            {
                _btnVisibility = value;
                RaisePropertyChanged("BtnVisibility");
            }
        }
        #endregion

        /// <summary>
        /// 设置城市
        /// </summary>
        private void SetCitys()
        {
            if (Constant.ConstantValue.CityList.Count > 0)
            {
                return;
            }
            XDocument xdoc = XDocument.Load("Constant/Pro_City.xml");
            List<Citys> citysList = new List<Citys>();
            foreach (XElement rootElement in xdoc.Descendants("Province"))
            {
                Citys citys = new Citys();
                citys.Pro = (string)rootElement.Element("pro_name");
                XElement nodesElement = rootElement.Element("City");
                foreach (XElement nodeElement in nodesElement.Elements("city_name"))
                {
                    citys.CityList.Add(nodeElement.Value);
                }
                citysList.Add(citys);
            }

            Constant.ConstantValue.CityList = citysList;
        }
    }
}
