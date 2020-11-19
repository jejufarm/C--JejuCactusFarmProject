using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{
    public class ContentControlViewModel : NotifyPropertyChanged
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

        private static ContentControlViewModel instance;

        public static ContentControlViewModel GetInstance()
        {
            if (instance == null)
                instance = new ContentControlViewModel();

            return instance;
        }
    }
}
