using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
using JejuFarm_Receipt_Project.SubWindow.ContentWindow.SettingsWindow;
using System;
using System.Drawing.Printing;
using System.Printing;
using System.Windows.Controls;

namespace JejuFarm_Receipt_Project.SubWindow.ContentWindow
{
    /// <summary>
    /// SettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingWindow : UserControl
    {
        public SettingWindow()
        {
            InitializeComponent();
            SettingPage.DataContext = SettingPageControlViewModel.GetInstance();
        }

        private void SettingListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                dynamic meta_data = sender as dynamic;
                string setting = meta_data.SelectedItem.Name as string;
                switch (setting)
                {
                    case "PrinterSetting":
                        {
                            SettingPageControlViewModel.GetInstance().Page = new PrinterSettingWindow();
                            break;
                        }
                    case "BluetoothSetting":
                        {
                            SettingPageControlViewModel.GetInstance().Page = new BluetoothSettingWindow();
                            break;
                        }
                    case "CactusSetting":
                        {
                            SettingPageControlViewModel.GetInstance().Page = new CactusSettingWindow();
                            break;
                        }
                    case "ProgramSetting":
                        {
                            SettingPageControlViewModel.GetInstance().Page = new ProgramSettingWindow();
                            break;
                        }
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {

            }
        }
    }
}

