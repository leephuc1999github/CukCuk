using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    /// <summary>
    /// Cờ Required yêu cầu không để trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    /// <summary>
    /// Cờ Duplication thông báo thông tin không được trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Duplication : Attribute
    {

    }

    /// <summary>
    /// Cờ LengthAndFormat yêu cầu thông tin phải đúng độ dài và định dạng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]

    public class LengthAndFormat : Attribute
    {
        #region Property
        /// <summary>
        /// Yêu cầu độ dài
        /// </summary>
        public int ValidLength { get; set; }

        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Yêu cầu đúng định dạng
        /// </summary>
        public string Required { get; set; }
        #endregion

        #region Constructor
        public LengthAndFormat(int validLength, string errorMsg, string required)
        {
            this.ValidLength = validLength;
            this.ErrorMsg = errorMsg;
            this.Required = required;
        }
        #endregion
    }

    /// <summary>
    /// Cờ Email yêu cầu thông tin email ohair đúng đinh dạng
    /// </summary>
    public class Email : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotMapped : Attribute
    {

    }

    /// <summary>
    /// Các properties chung
    /// </summary>
    /// CreatedBy : LP(6/8)
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo bản ghi 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
