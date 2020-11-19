using JejuFarm_Receipt_Project.Mutex;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JejuFarm_Receipt_Project
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private RunOneInstance runOneApp = new RunOneInstance();

        protected override void OnStartup(StartupEventArgs e)
        {
            if (runOneApp.CreateOnlyOneMutex(null) == false)
            {
                MessageBox.Show("이미 프로그램이 실행중입니다.\r\n잠시후 다시 시도해주세요.", "WPF Program", MessageBoxButton.OK);
                Environment.Exit(0);
                return;
            }
            base.OnStartup(e);
        }
    }
}
