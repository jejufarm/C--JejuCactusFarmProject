
using BluetoothCore;
using IniSettings;
using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using JejuFarm_Receipt_Project.SubWindow.ContentWindow;
using PrinterCore;
using ProgramCore.ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace JejuFarm_Receipt_Project
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class MainWindow : Window
    {

        public static Bluetooth bt;
        private bool shutdown = true;
        private Printer printer;

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
            printer = new Printer();
            bt = new Bluetooth();
            bt.send = RecvBluetooth;
            bt.print = Print;
        }

        private void Print(List<BasketListForm> list)
        {
            int count = 0;
            int total = 0;
            foreach (var item in list)
            {
                count += item.Count;
                total += item.Total;
            }
            ReceiptFormViewModel.GetInstance().Count = count;
            ReceiptFormViewModel.GetInstance().Total = total;
            ReceiptFormViewModel.GetInstance().Time = DateTime.Now.ToString("yyyy년MM월dd일 h시mm분ss초");

            ReceiptFormViewModel.GetInstance().List.Clear();
            foreach (var item in list)
                ReceiptFormViewModel.GetInstance().List.Add(item);

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                printer.Print(new PrinterFormWindow());
            }));
        }

        private void RecvBluetooth(string msg)
        {

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                if (msg == "bluetooth_connect")
                {
                    BluetoothStatus.IsEnabled = true;
                }
                else if (msg == "bluetooth_disconnect")
                {
                    BluetoothStatus.IsEnabled = false;
                }

            }));
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
