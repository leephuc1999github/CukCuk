using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class InventoryItemGroup : BaseEntity
    {
        /// <summary>
        /// Nhóm thực đơn
        /// </summary>
        [Key]
        public Guid IdInventoryItemGroup { get; set; }

        /// <summary>
        /// Mã nhóm thực đơn
        /// </summary>
        [Required]
        [Duplication]
        public string CodeInventoryItemGroup { get; set; }

        /// <summary>
        /// Tên nhớm thực đơn
        /// </summary>
        [Required]
        public string NameInventoryItemGroup { get; set; }

        /// <summary>
        /// Loại nhóm thực đơn
        /// </summary>
        public string TypeInventoryItemGroup { get; set; }

        /// <summary>
        /// Chế biến tại
        /// </summary>
        public string ProcessingIn { get; set; }

        /// <summary>
        /// Mô tả nhóm thực đơn
        /// </summary>
        public string DescriptionInventoryItemGroup { get; set; }
    }
}
