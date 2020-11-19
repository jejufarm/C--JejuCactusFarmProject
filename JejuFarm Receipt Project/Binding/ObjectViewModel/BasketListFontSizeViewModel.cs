using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{
    public class BasketListFontSizeViewModel : NotifyPropertyChanged
    {
        private int _fontsize = 20;

        public int FontSize
        {
            get
            {
                return _fontsize;
            }
            set
            {
                _fontsize = value;
                OnPropertyChanged("FontSize");
            }
        }

        private static BasketListFontSizeViewModel instance;

        public static BasketListFontSizeViewModel GetInstance()
        {
            if (instance == null)
                instance = new BasketListFontSizeViewModel();
            return instance;
        }
    }
}
