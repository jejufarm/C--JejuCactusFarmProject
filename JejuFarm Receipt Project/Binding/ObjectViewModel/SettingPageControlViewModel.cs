using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{
    public class SettingPageControlViewModel : NotifyPropertyChanged
    {
        private UserControl _page;
        public UserControl Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
                OnPropertyChanged("Page");
            }
        }

        private static SettingPageControlViewModel instance;

        public static SettingPageControlViewModel GetInstance()
        {
            if (instance == null)
                instance = new SettingPageControlViewModel();

            return instance;
        }
    }
}
