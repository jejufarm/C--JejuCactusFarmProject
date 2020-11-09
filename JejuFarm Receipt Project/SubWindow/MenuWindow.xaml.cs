using JejuFarm_Receipt_Project.Binding.ObjectModel;
using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using JejuFarm_Receipt_Project.SubWindow.ContentWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JejuFarm_Receipt_Project.SubWindow
{
    /// <summary>
    /// MenuWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuWindow : UserControl
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControlViewModel.GetInstance().Page = new ReceiptWindow();
        }

        private void HomeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BasketListModel.Delete(0);
            ContentControlViewModel.GetInstance().Page = new ReceiptWindow();
        }
    }
}
