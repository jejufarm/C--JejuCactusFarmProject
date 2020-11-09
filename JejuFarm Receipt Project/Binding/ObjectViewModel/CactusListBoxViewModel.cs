using JejuFarm_Receipt_Project.Binding.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{
    public class CactusListBoxViewModel
    {
        private string _title;
        private int _price;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        private static ObservableCollection<CactusListBoxViewModel> instance;

        public static ObservableCollection<CactusListBoxViewModel> GetInstance()
        {
            if (instance == null)
                instance = new ObservableCollection<CactusListBoxViewModel>();
            return instance;
        }

        public static void SetSource(ObservableCollection<CactusListBoxViewModel> items)
        {
            GetInstance().Clear();
            foreach (var item in items)
            {
                GetInstance().Add(new CactusListBoxViewModel()
                {
                    Title = item.Title,
                    Price = item.Price
                });
            }
        }
    }
}
