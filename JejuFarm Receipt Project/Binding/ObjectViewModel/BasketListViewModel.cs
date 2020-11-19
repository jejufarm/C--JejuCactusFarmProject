using ProgramCore.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{

    public class BasketListModel
    {
        private static List<BasketListForm> instance;

        public static List<BasketListForm> GetInstance()
        {
            if (instance == null)
                instance = new List<BasketListForm>();
            return instance;
        }

        public static void Insert(BasketListForm item)
        {
            GetInstance().Add(new BasketListForm()
            {
                Index = GetInstance().Count + 1, // 자동설정
                Title = item.Title,
                Count = item.Count,
                Price = item.Price,
                Total = item.Count * item.Price // 자동설정
            });

            BasketListViewModel.GetInstance().Add(new BasketListViewModel()
            {
                Index = GetInstance()[GetInstance().Count - 1].Index,
                Title = GetInstance()[GetInstance().Count - 1].Title,
                Count = GetInstance()[GetInstance().Count - 1].Count,
                Price = GetInstance()[GetInstance().Count - 1].Price,
                Total = GetInstance()[GetInstance().Count - 1].Total
            });
        }

        // List를 ObservableCollection로 바꾸면 이런 처리는 필요 없을 것 같음.
        public static void Delete(int index)
        {
            if (index == 0)
            {
                GetInstance().Clear();
                BasketListViewModel.SetSource(new ObservableCollection<BasketListViewModel>());
                return;
            }
            ObservableCollection<BasketListViewModel> temp = new ObservableCollection<BasketListViewModel>();
            try
            {
                GetInstance().RemoveAt(index - 1);
                for (int idx = index - 1; idx < GetInstance().Count; idx++)
                    GetInstance()[idx].Index = idx + 1;

                foreach (var item in GetInstance())
                    temp.Add(new BasketListViewModel()
                    {
                        Index = item.Index,
                        Title = item.Title,
                        Count = item.Count,
                        Price = item.Price,
                        Total = item.Total
                    });

                BasketListViewModel.SetSource(temp);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("항목이 존재하지 않습니다.");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "알 수 없는 오류");
            }
        }
    }
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
