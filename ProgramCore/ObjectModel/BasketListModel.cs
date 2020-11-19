using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramCore.ObjectModel
{
    public class BasketListForm
    {
        public int Index { get; set; }  // 번호
        public string Title { get; set; } // 이름
        public int Count { get; set; } // 수량
        public int Price { get; set; } // 단가
        public int Total { get; set; } // 합계
    }

}
