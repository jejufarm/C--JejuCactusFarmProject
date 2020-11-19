using ProgramCore.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{
    public class ReceiptFormViewModel : NotifyPropertyChanged
    {
        private string _time;
        private int _total;
        private int _count;
        private ObservableCollection<BasketListForm> _list = new ObservableCollection<BasketListForm>();

        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("Time");   
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

        public ObservableCollection<BasketListForm> List
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                OnPropertyChanged("List");
            }
        }

        private static ReceiptFormViewModel instance;

        public static ReceiptFormViewModel GetInstance()
        {
            if (instance == null)
                instance = new ReceiptFormViewModel();

            //instance.Time = DateTime.Now.ToString("yyyy년MM월dd일 h시mm분ss초"); // 시간 자동 갱신
            return instance;
        }
    }
}
