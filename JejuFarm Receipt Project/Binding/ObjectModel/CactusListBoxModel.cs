using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JejuFarm_Receipt_Project.Binding.ObjectModel
{
    public class CactusListForm
    {
        public string Title { get; set; }
        public int Price { get; set; }
    }

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
    }
}
