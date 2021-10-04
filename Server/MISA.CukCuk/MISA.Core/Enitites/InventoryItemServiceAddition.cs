using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class InventoryItemServiceAddition : BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public Guid IdInventoryItemServiceAddition { get; set; }

        /// <summary>
        /// Id món ăn
        /// </summary>
        [Required]
        public Guid InventoryItemId { get; set; }

        /// <summary>
        /// SỞ thích phục vụ
        /// </summary>
        [Required]
        public Guid ServiceAdditionId { get; set; }
    }
}
