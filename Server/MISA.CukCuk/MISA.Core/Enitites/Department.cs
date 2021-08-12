using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    /// <summary>
    /// Lớp phòng ban
    /// </summary>
    /// CreatedBy : LP(6/8)
    public class Department : BaseEntity
    {
        #region Declare
        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Method 
        #endregion
    }
}
