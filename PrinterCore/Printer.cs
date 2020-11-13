using IniSettings;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Media;

namespace PrinterCore
{
    public class Printer
    {
        private static PrintDialog print = null;
        public Printer()
        {
            print = new PrintDialog();
            INISetting ini = new INISetting();
            print.PrintTicket.PageMediaSize = new PageMediaSize(ini.LoadPrintPageSize());
            print.PrintTicket.CopyCount = ini.LoadPrintPageCount();
        }

        public void Print(Visual form)
        {
            print.PrintVisual(form, "제주농원");
        }

    }
}
