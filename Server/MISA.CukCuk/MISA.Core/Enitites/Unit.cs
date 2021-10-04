using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class Unit : BaseEntity
    {
        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [Key]
        public Guid IdUnit { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [Required]
        public string NameUnit { get; set; }

        /// <summary>
        /// Mô tả đơn vị
        /// </summary>
        public string DescriptionUnit { get; set; }
    }
}
