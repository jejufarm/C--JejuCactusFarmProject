
using IniSettings;
using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using PrinterCore;
using ProgramCore;
using ProgramCore.ObjectModel;
using ProgramServices;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace JejuFarm_Receipt_Project.SubWindow.ContentWindow
{
    /// <summary>
    /// ReceiptWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReceiptWindow : UserControl
    {
        private double[] basket_auto_size = { 0 };
        private Printer printer = new Printer();


        private void InitBinding()
        {
            BasketListView.ItemsSource = BasketListViewModel.GetInstance();
            CactusListBox.ItemsSource = CacutsListBoxModel.GetInstance();

            BasketListView.DataContext = BasketListFontSizeViewModel.GetInstance();
            CactusListBox.DataContext = ListBoxFontSizeViewModel.GetInstance();
            CountListBox.DataContext = ListBoxFontSizeViewModel.GetInstance();
            INISetting ini = new INISetting();
            basket_auto_size = ini.LoadListViewWidth();

        }
        public ReceiptWindow()
        {
            InitializeComponent();
            InitBinding();
            CacutsListBoxModel.Load(ProgramService.db.LoadCactusList());


            INISetting ini = new INISetting();
           
            ListBoxFontSizeViewModel.GetInstance().FontSize = ini.LoadListBoxFontSize();
            BasketListFontSizeViewModel.GetInstance().FontSize = ini.LoadListViewFontSize();
            for (int i = 1; i <= 50; i++)
                CountListBox.Items.Add(i);
        }

        private void BasketListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double remainingSpace = BasketListView.ActualWidth;
            if (remainingSpace > 0)
                for (int idx = 0; idx < 5; idx++)
                    (BasketListView.View as GridView).Columns[idx].Width = Math.Ceiling(remainingSpace / basket_auto_size[idx]);
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
            dynamic count = CountListBox as dynamic;
            if (BasketListModel.GetInstance().Count >= 21)
            {
                MessageBox.Show("22개부터는 추가하실 수 없습니다.", "경고");
                return;
            }
            if (Item.SelectedItems.Count > 0 && count.SelectedItems.Count > 0)
            {
                BasketListModel.Insert(new BasketListForm()
                {
                    Title = Item.SelectedItems[0].Title,
                    Price = Item.SelectedItems[0].Price,
                    Count = count.SelectedItems[0]
                });

                CactusListBox.SelectedItem = null;
                CountListBox.SelectedItem = null;
            }
            else
            {
                // TODO: 항목선택
                //MessageBox.Show("항목이 선택되지 않았습니다.", "경고");
            }
        }

        private void PrinterButton_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            int total = 0;
            foreach(var item in BasketListModel.GetInstance())
            {
                count += item.Count;
                total += item.Total;
            }
            ReceiptFormViewModel.GetInstance().Count = count;
            ReceiptFormViewModel.GetInstance().Total = total;
            ReceiptFormViewModel.GetInstance().Time = DateTime.Now.ToString("yyyy년MM월dd일 h시mm분ss초");

            ReceiptFormViewModel.GetInstance().List.Clear();
            foreach (var item in BasketListModel.GetInstance())
                ReceiptFormViewModel.GetInstance().List.Add(item);

            printer.Print(new PrinterFormWindow());
        }
    }
}
