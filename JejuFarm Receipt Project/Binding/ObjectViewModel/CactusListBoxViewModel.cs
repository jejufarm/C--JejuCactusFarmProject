using Microsoft.VisualBasic;
using ProgramCore.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JejuFarm_Receipt_Project.Binding.ObjectViewModel
{
    public class CacutsListBoxModel
    {
        private static List<CactusListForm> instance;

        public static List<CactusListForm> GetInstance()
        {
            if (instance == null)
                instance = new List<CactusListForm>();
            return instance;
        }

        public static void Load(List<CactusListForm> obj)
        {
            GetInstance().Clear();
            ObservableCollection<CactusListBoxViewModel> temp = new ObservableCollection<CactusListBoxViewModel>();
            foreach (var item in obj)
            {
                GetInstance().Add(item);
                temp.Add(new CactusListBoxViewModel()
                {
                    Title = item.Title ,
                    Price = item.Price
                });
            }

            CactusListBoxViewModel.SetSource(temp);
        }
        public static int InsertItem(string title, int price)
        {
            int cycle = 0;
            foreach(var item in GetInstance())
            {
                int temp = Convert.ToInt32(Strings.Split(item.Key, "Cactus")[1]);
                if (temp == cycle)
                {
                    cycle++;
                    continue;
                }
                else
                    break;
                
            }
            CactusListForm tmp = new CactusListForm()
            {
                Key = "Cactus" + cycle,
                Title = title,
                Price = price
            };

            GetInstance().Add(tmp);
            CactusListBoxViewModel.GetInstance().Add(new CactusListBoxViewModel()
            {
                Title = tmp.Title,
                Price = tmp.Price
            });
            return cycle;
        }
        public static bool UpdateItem(int idx, string title, int price)
        {
            GetInstance()[idx].Title = title;
            GetInstance()[idx].Price = price;
            CactusListBoxViewModel.GetInstance()[idx].Title = title;
            CactusListBoxViewModel.GetInstance()[idx].Price = price;
            return true;
        }

        public static int DeleteItem(int idx)
        {
            int temp = Convert.ToInt32(Strings.Split(GetInstance()[idx].Key,"Cactus")[1]);
            GetInstance().RemoveAt(idx);
            CactusListBoxViewModel.GetInstance().RemoveAt(idx);
            return temp;
        }
    }
    public class CactusListBoxViewModel : NotifyPropertyChanged
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
                OnPropertyChanged("Title");
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
