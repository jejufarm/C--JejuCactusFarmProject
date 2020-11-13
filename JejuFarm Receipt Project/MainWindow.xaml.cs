
using BluetoothCore;
using IniSettings;
using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using JejuFarm_Receipt_Project.SubWindow.ContentWindow;
using System;
using System.Diagnostics;
using System.Windows;

namespace JejuFarm_Receipt_Project
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    
    public partial class MainWindow : Window
    {
        //Bluetooth bt;
        private bool shutdown = true;
        private void InitBinding()
        {
            ContentPage.DataContext = ContentControlViewModel.GetInstance(); // ContentPageBinding
        }
        public MainWindow()
        {
            InitializeComponent();
            InitBinding();
            INISetting ini = new INISetting();
            shutdown = ini.LoadShutDown();
            BluetoothStatus.IsEnabled = false;
            ContentControlViewModel.GetInstance().Page = new ReceiptWindow();
            //bt = new Bluetooth();
            
        }

        private void WindowsCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (shutdown)
                Process.Start("shutdown", "/s /f /t 0");
        }
    }
}
