using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class BaseEntityPaging<T>
    {
        /// <summary>
        /// Tổng số lượng bản ghi
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Thứ tự trang
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Độ dài bản ghi trên trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
