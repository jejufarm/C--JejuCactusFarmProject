using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JejuFarm_Receipt_Project.Binding.ObjectModel
{
    public class BasketListForm
    {
        public int Index { get; set; }  // 번호
        public string Title { get; set; } // 이름
        public int Count { get; set; } // 수량
        public int Price { get; set; } // 단가
        public int Total { get; set; } // 합계
    }

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
                Index = GetInstance().Count + 1 , // 자동설정
                Title = item.Title ,
                Count = item.Count ,
                Price = item.Price ,
                Total = item.Count * item.Price // 자동설정
            });

            BasketListViewModel.GetInstance().Add(new BasketListViewModel()
            {
                Index = GetInstance()[GetInstance().Count - 1].Index,
                Title = GetInstance()[GetInstance().Count - 1].Title,
                Count = GetInstance()[GetInstance().Count - 1].Count ,
                Price = GetInstance()[GetInstance().Count - 1].Price ,
                Total = GetInstance()[GetInstance().Count - 1].Total
            });
        }

        public static void Delete(int index)
        {
            if(index == 0)
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
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("항목이 존재하지 않습니다.");
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message,"알 수 없는 오류");
            }
        }
    }
}
