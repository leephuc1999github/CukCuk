using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    /// <summary>
    /// Lớp vị trí
    /// </summary>
    /// CreatedBy : LP(6/8)
    public class Position : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }
    }
}
