using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using JejuFarm_Receipt_Project.SubWindow.ContentWindow;
using System.Windows;

namespace JejuFarm_Receipt_Project
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private void InitBinding()
        {
            ContentPage.DataContext = ContentControlViewModel.GetInstance(); // ContentPageBinding
        }
        public MainWindow()
        {
            InitializeComponent();
            InitBinding();

            BluetoothStatus.IsEnabled = false;
            ContentControlViewModel.GetInstance().Page = new ReceiptWindow();
        }

        private void WindowsCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
