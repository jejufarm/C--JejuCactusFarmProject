using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace JeJuFarmProject
{
    class Function
    {
        public int Ubound(string[] Body)
        {
            int Count = 0;
            try
            {
                for (Count = 0; ; Count++)
                {
                    Strings.InStr(Body[Count], "");
                }
            }
            catch (Exception)
            {
                return Count;
            }
        }

        public int InstrSeach(string Body, string Seach)
        {
            int Count = 0;
            int Postion = 0;
            if (Strings.InStr(Body, Seach) != 0)
            {
                Postion = Strings.InStr(Body, Seach);
                for (Count = 1; ; Count++)
                {
                    if (Strings.InStr(Strings.Mid(Body, Postion + 1), Seach) != 0)
                    {
                        Postion = Postion + Strings.InStr(Strings.Mid(Body, Postion + 1), Seach);
                    }
                    else
                        return Count;
                }
            }
            else
                return 0;
        }
    }
}
