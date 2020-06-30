using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace JeJuFarmProject
{
    class JejuFarm
    {
        object misValue = System.Reflection.Missing.Value;
        Microsoft.Office.Interop.Excel.Workbook workbook = null;
        Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
        Microsoft.Office.Interop.Excel.Application application = null;
        //Excel이 닫히는 시점에 실행되어 각 인스턴스의 메모리 해제를 강제로 합니다.
        //이때 NAR로 구현된 메쏘드를 사용합니다.
        void Application_ExcelDeactivate(Microsoft.Office.Interop.Excel.Workbook Wb)
        {
            try
            {
                this.application.Quit();
                this.NAR(this.worksheet);
                this.workbook.Close(false, Type.Missing, Type.Missing);
                this.NAR(this.workbook);
                this.NAR(this.application);
                this.application = null;
            }
            catch { };
        }
        //마샬링된 COM 객체를 해제하기 위한 메쏘드 입니다.
        public bool GetVisible()
        {
            if(application.Visible == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetPrintExcel()
        {
            string Defprinter = null;
            Defprinter = application.ActivePrinter;
            //application.ActivePrinter = "Microsoft XPS Document Writer";
            var _with = worksheet.PageSetup;
            _with.PaperSize = Excel.XlPaperSize.xlPaperA5;
            _with.Orientation = Excel.XlPageOrientation.xlPortrait;

            _with.FitToPagesTall = 1;
            _with.FitToPagesWide = 1;

            _with.LeftMargin = application.InchesToPoints(0.52);
            _with.RightMargin = application.InchesToPoints(0);
            _with.TopMargin = application.InchesToPoints(0.32);
            _with.BottomMargin = application.InchesToPoints(0);
            _with.HeaderMargin = application.InchesToPoints(0);
            _with.FooterMargin = application.InchesToPoints(0);


            worksheet.PrintOutEx(1, 1, 2, false, false, false, false);
            //application.ActivePrinter = Defpr
            application.ActivePrinter = Defprinter;
        }
        private void NAR(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

            }
            catch { }
            finally
            {
                obj = null;
            }
        }
        public void SetVisibleExcel(bool Str)
        {
            if (Str == true)
            {
                application.Visible = true;
            }
            else
            {
                application.Visible = false;
            }
        }
        public void SetDataExcel(object[,] Cactus)
        {
            //....................................  
            //worksheet를 가지고 지지고 볶는다.
            for (int i = 1; i <= 21; i++)
            {
                worksheet.Cells[i + 10, 2] = Cactus[i + 10, 1];
                worksheet.Cells[i + 10, 3] = Cactus[i + 10, 2];
                worksheet.Cells[i + 10, 4] = Cactus[i + 10, 3];
                worksheet.Cells[i + 10, 5] = Cactus[i + 10, 4];
            }
            worksheet.Cells[33, 4] = Cactus[33, 4];
            worksheet.Cells[9, 3] = Cactus[9, 3];
            worksheet.Cells[7, 3] = Cactus[7, 3];
            //....................................  
            //사용자에게 Excel 공개    
        }
        public void OpenExcel()
        {
            
            //다음 IF문을 통해 Excel이 하나만 열리도록 합니다.
            if (this.application != null)
            {
                return;
            }
            application = new Microsoft.Office.Interop.Excel.Application();
            //Excel을 사용자가 닫으면 그 이벤트를 받기 위해 핸들러를 걸어줍니다.
            //핸들러(메쏘드) 이름은 Application_ExcelDeactivate 입니다.
            //application.WindowDeactivate += new Excel.AppEvents_WindowDeactivateEventHandler(Application_ExcelDeactivate);
            string excelFile = System.Environment.CurrentDirectory + @"\Main.xlsm";
            workbook = (Excel.Workbook)(application.Workbooks.Open(excelFile, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing));
            worksheet = workbook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
        }
        public void EndExcel()
        {
            Application_ExcelDeactivate(workbook);
        }
        public object[,] GetExcelFrmData()
        {
            Excel.Range rng = worksheet.UsedRange;
            object[,] data = rng.Value;
            for(int r = 1; r <= data.GetLength(0); r++)
            {
                for(int c = 1; c <= data.GetLength(1); c++)
                {
                    if (r >= 11 && c >= 2 && r <= 31 && c <= 6)
                        data[r, c] = null;
                }
            }
            return data;
        }
    }
}
