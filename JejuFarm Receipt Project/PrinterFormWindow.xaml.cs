using JejuFarm_Receipt_Project.Binding.ObjectViewModel;
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

namespace JejuFarm_Receipt_Project
{
    /// <summary>
    /// PrinterFormWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PrinterFormWindow : UserControl
    {
        public PrinterFormWindow()
        {
            InitializeComponent();

            #region Binding
            CountText.DataContext = ReceiptFormViewModel.GetInstance();
            SumText1.DataContext = ReceiptFormViewModel.GetInstance();
            SumText2.DataContext = ReceiptFormViewModel.GetInstance();
            TimeText.DataContext = ReceiptFormViewModel.GetInstance();

            IndexText0.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText1.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText2.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText3.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText4.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText5.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText6.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText7.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText8.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText9.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText10.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText11.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText12.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText13.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText14.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText15.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText16.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText17.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText18.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText19.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText20.DataContext = ReceiptFormViewModel.GetInstance();
            IndexText21.DataContext = ReceiptFormViewModel.GetInstance();

            TitleText0.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText1.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText2.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText3.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText4.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText5.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText6.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText7.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText8.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText9.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText10.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText11.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText12.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText13.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText14.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText15.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText16.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText17.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText18.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText19.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText20.DataContext = ReceiptFormViewModel.GetInstance();
            TitleText21.DataContext = ReceiptFormViewModel.GetInstance();

            CountText0.DataContext = ReceiptFormViewModel.GetInstance();
            CountText1.DataContext = ReceiptFormViewModel.GetInstance();
            CountText2.DataContext = ReceiptFormViewModel.GetInstance();
            CountText3.DataContext = ReceiptFormViewModel.GetInstance();
            CountText4.DataContext = ReceiptFormViewModel.GetInstance();
            CountText5.DataContext = ReceiptFormViewModel.GetInstance();
            CountText6.DataContext = ReceiptFormViewModel.GetInstance();
            CountText7.DataContext = ReceiptFormViewModel.GetInstance();
            CountText8.DataContext = ReceiptFormViewModel.GetInstance();
            CountText9.DataContext = ReceiptFormViewModel.GetInstance();
            CountText10.DataContext = ReceiptFormViewModel.GetInstance();
            CountText11.DataContext = ReceiptFormViewModel.GetInstance();
            CountText12.DataContext = ReceiptFormViewModel.GetInstance();
            CountText13.DataContext = ReceiptFormViewModel.GetInstance();
            CountText14.DataContext = ReceiptFormViewModel.GetInstance();
            CountText15.DataContext = ReceiptFormViewModel.GetInstance();
            CountText16.DataContext = ReceiptFormViewModel.GetInstance();
            CountText17.DataContext = ReceiptFormViewModel.GetInstance();
            CountText18.DataContext = ReceiptFormViewModel.GetInstance();
            CountText19.DataContext = ReceiptFormViewModel.GetInstance();
            CountText20.DataContext = ReceiptFormViewModel.GetInstance();
            CountText21.DataContext = ReceiptFormViewModel.GetInstance();

            PriceText0.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText1.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText2.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText3.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText4.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText5.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText6.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText7.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText8.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText9.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText10.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText11.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText12.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText13.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText14.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText15.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText16.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText17.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText18.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText19.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText20.DataContext = ReceiptFormViewModel.GetInstance();
            PriceText21.DataContext = ReceiptFormViewModel.GetInstance();

            TotalText0.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText1.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText2.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText3.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText4.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText5.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText6.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText7.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText8.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText9.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText10.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText11.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText12.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText13.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText14.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText15.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText16.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText17.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText18.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText19.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText20.DataContext = ReceiptFormViewModel.GetInstance();
            TotalText21.DataContext = ReceiptFormViewModel.GetInstance();
            #endregion
            
        }
    }
}
