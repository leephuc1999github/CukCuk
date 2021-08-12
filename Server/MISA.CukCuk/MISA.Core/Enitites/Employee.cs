using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    /// <summary>
    /// Lớp nhân viên
    /// </summary>
    /// CreatedBy : LP(6/8)
    public class Employee : BaseEntity
    {
        #region Declare
        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        [Duplication]
        [LengthAndFormat(10, "Mã nhân viên phải có dạng NV-xxxxxxx", "KH-")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Duplication]
        [Email]
        public string Email { get; set; }

        /// <summary>
        /// Số chứng minh thử, thẻ căn cước
        /// </summary>
        [Required]
        [Duplication]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        [Required]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Số điện thoại 
        /// </summary>
        [Required]
        [Duplication]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        public float Salary { get; set; }

        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Tình trạng công việc 
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Tên chức vụ 
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Họ 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public int? MartialStatus { get; set; }


        /// <summary>
        /// Trình độ giáo dục
        /// </summary>
        public string EducationalBackground { get; set; }

        /// <summary>
        /// Chất lượng 
        /// </summary>
        public Guid? QualificationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PersonalTaxCode { get; set; }
        #endregion


        #region Constructor
        #endregion

        #region Method
        #endregion

    }
}
