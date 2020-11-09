using JejuFarm_Receipt_Project.Binding.ObjectModel;
using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JejuFarm_Receipt_Project.SubWindow.ContentWindow
{
    /// <summary>
    /// ReceiptWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReceiptWindow : UserControl
    {
        private double[] basket_auto_size = { 8, 3, 6, 5, 5 };
        private int count = 0;
        private void InitBinding()
        {
            BasketListView.ItemsSource = BasketListViewModel.GetInstance();
            CactusListBox.ItemsSource = CacutsListBoxModel.GetInstance();
        }
        public ReceiptWindow()
        {
            InitializeComponent();
            InitBinding();

            List<CactusListForm> list = new List<CactusListForm>();
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            list.Add(new CactusListForm() { Title = "성성환", Price = 45000 });
            list.Add(new CactusListForm() { Title = "소정", Price = 35000 });
            list.Add(new CactusListForm() { Title = "금황환", Price = 50000 });
            list.Add(new CactusListForm() { Title = "용심목", Price = 70000 });
            list.Add(new CactusListForm() { Title = "설황", Price = 30000 });
            CacutsListBoxModel.Load(list);

        }

        private void BasketListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double remainingSpace = BasketListView.ActualWidth;
            if (remainingSpace > 0)
                for (int idx = 0; idx < 5; idx++)
                    (BasketListView.View as GridView).Columns[idx].Width = Math.Ceiling(remainingSpace / basket_auto_size[idx]);
        }

        private void TempButton_Click(object sender, RoutedEventArgs e)
        {
            BasketListModel.Insert(new BasketListForm() { Title = "선인장" + ++count, Count = 5, Price = 150000 });
        }


        private void BasketListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic meta_data = sender as dynamic;
            if (meta_data.SelectedIndex > -1)
            {
                BasketListModel.Delete(meta_data.SelectedIndex + 1);
            }
            else
            {
                MessageBox.Show("삭제할 항목을 선택해주세요.", "알림");
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic Item = CactusListBox as dynamic;
            if (Item.SelectedItems.Count > 0)
            {
                BasketListModel.Insert(new BasketListForm()
                {
                    Title = Item.SelectedItems[0].Title,
                    Price = Item.SelectedItems[0].Price,
                    Count = 5
                });

                CactusListBox.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("항목이 선택되지 않았습니다.", "경고");
            }
        }

    }
}
