using Microsoft.VisualBasic;
using ProgramCore.ObjectModel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Printing;
using System.IO;

namespace IniSettings
{
    /// <summary>
    /// KEY값은 대소문자를 구별안함.
    /// </summary>
    public class INISetting
    {
        #region DLLImport
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion

        private string Path;
        public INISetting()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\JejuReceiptSetting\\Settings.ini";

            if (!ExistINI())
            {
                WriteProerty("ProgramSettings", "MAX_PRODUCT", "100");
                WriteProerty("ProgramSettings", "LISTVIEW_FONTSIZE", "25");
                WriteProerty("ProgramSettings", "LISTBOX_FONTSIZE", "20");
                WriteProerty("ProgramSettings", "SHUT_DOWN", "1");

                WriteProerty("ProgramSettings", "LISTVIEW_1_WIDTH", "8.0");
                WriteProerty("ProgramSettings", "LISTVIEW_2_WIDTH", "3.0");
                WriteProerty("ProgramSettings", "LISTVIEW_3_WIDTH", "6.0");
                WriteProerty("ProgramSettings", "LISTVIEW_4_WIDTH", "5.0");
                WriteProerty("ProgramSettings", "LISTVIEW_5_WIDTH", "5.0");

                WriteProerty("CactusList", "Cactus0", "선인장1 100000");
                WriteProerty("CactusList", "Cactus1", "선인장2 200000");
                WriteProerty("CactusList", "Cactus2", "선인장3 300000");

                WriteProerty("Printer", "PageSize", "A5");
                WriteProerty("Printer", "PageCount", "2");
            }

        }
        public INISetting(string path)
        {
            Path = path;
        }

        private bool ExistINI()
        {
            return new FileInfo(Path).Exists;
        }

        public void WriteProerty(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        // TODO: Exception 처리 필요
        public string GetProperty(string section, string key)
        {
            StringBuilder temp = new StringBuilder(256);
            int ret = GetPrivateProfileString(section, key, "", temp, 256, Path);
            if (ret == 0)
                return null;
            return temp.ToString();
        }

        // TODO: Exception 처리 필요
        public List<CactusListForm> LoadCactusList()
        {
            List<CactusListForm> list = new List<CactusListForm>();
            string data = GetProperty("ProgramSettings", "MAX_PRODUCT");

            if (data == null)
                return null;

            int count = Convert.ToInt32(data);
            for (int i = 0; i < count; i++)
            {
                data = GetProperty("CactusList", "Cactus" + i);
                if (data != null)
                {
                    list.Add(new CactusListForm()
                    {
                        Index = i,
                        Title = Strings.Split(data, " ")[0],
                        Price = Convert.ToInt32(Strings.Split(data, " ")[1])
                    });
                }
            }

            return list;
        }

        // TODO: Exception 처리 필요
        public int LoadListBoxFontSize()
        {
            string data = GetProperty("ProgramSettings", "LISTBOX_FONTSIZE");
            if (data == null)
                return 20; // 기본 값
            else
                return Convert.ToInt32(data);
        }

        // TODO: Exception 처리 필요
        public int LoadListViewFontSize()
        {
            string data = GetProperty("ProgramSettings", "LISTVIEW_FONTSIZE");
            if (data == null)
                return 30; // 기본 값
            else
                return Convert.ToInt32(data);
        }

        // TODO: Exception 처리 필요
        // TODO: No Static
        public double[] LoadListViewWidth()
        {
            double[] default_size = { 8, 3, 6, 5, 5 };
            double[] width = new double[5];
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    double temp = Convert.ToDouble(GetProperty("ProgramSettings", "LISTVIEW_" + (i + 1) + "_WIDTH"));

                    width[i] = temp <= 0 ? default_size[i] : temp;
                }
                catch (FormatException)
                {
                    width[i] = default_size[i];
                }
            }
            return width;
        }

        // TODO: Exception 처리 필요
        public PageMediaSizeName LoadPrintPageSize()
        {
            string data = GetProperty("Printer", "PageSize");
            switch (data)
            {
                case "A3":
                    {
                        return PageMediaSizeName.ISOA3;
                    }
                case "A4":
                    {
                        return PageMediaSizeName.ISOA4;
                    }
                case "A5":
                    {
                        return PageMediaSizeName.ISOA5;
                    }
                default:
                    {
                        return PageMediaSizeName.ISOA5; // 기본값
                    }
            }
        }

        // TODO: Exception 처리 필요
        public int LoadPrintPageCount()
        {
            int data = Convert.ToInt32(GetProperty("Printer", "PageCount"));
            return data == 0 ? 1 : data;
        }

        // TODO: No Static
        public bool LoadShutDown()
        {
            int data = Convert.ToInt32(GetProperty("ProgramSettings", "SHUT_DOWN"));
            return data == 0 ? false : true;
        }
    }
}
