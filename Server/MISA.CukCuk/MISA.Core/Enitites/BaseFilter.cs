using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class BaseFilter
    {

        /// <summary>
        /// Toán tử tìm kiếm 
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Giá trị tìm kiếm
        /// </summary>
        public string KeySearch { get; set; }

        /// <summary>
        /// Tên cột cần tìm
        /// </summary>
        public string NameProperty { get; set; }
    }
}
