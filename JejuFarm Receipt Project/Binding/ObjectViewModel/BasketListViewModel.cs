using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{
    public class BasketListViewModel : NotifyPropertyChanged
    {
        private int _index;  // 번호
        private string _title; // 이름
        private int _count; // 수량
        private int _price; // 단가
        private int _total; // 합계

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                OnPropertyChanged("Index");
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged("Count");
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
                OnPropertyChanged("Price");
            }
        }

        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }

        private static ObservableCollection<BasketListViewModel> instance;

        public static ObservableCollection<BasketListViewModel> GetInstance()
        {
            if (instance == null)
                instance = new ObservableCollection<BasketListViewModel>();
            return instance;
        }

        public static void SetSource(ObservableCollection<BasketListViewModel> items)
        {
            instance.Clear();
            foreach(var item in items)
            {
                GetInstance().Add(new BasketListViewModel()
                {
                    Index = item.Index ,
                    Title = item.Title ,
                    Count = item.Count ,
                    Price = item.Price ,
                    Total = item.Total
                });
            }
        }
    }
}
