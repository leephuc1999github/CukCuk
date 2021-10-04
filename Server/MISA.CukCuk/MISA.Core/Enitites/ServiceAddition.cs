using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class ServiceAddition : BaseEntity
    {
        /// <summary>
        /// Id sở thích phục vụ
        /// </summary>
        [Key]
        public Guid IdServiceAddition { get; set; }

        /// <summary>
        /// Tên sở thích phục vụ
        /// </summary>
        [Required]
        public string NameServiceAddition { get; set; }

        /// <summary>
        /// Thu thêm
        /// </summary>
        [Required]
        public int PriceServiceAddition { get; set; }
    }
}
