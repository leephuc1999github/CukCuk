using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class Picture : BaseEntity
    {
        /// <summary>
        /// Ảnh id
        /// </summary>
        [Key]
        public Guid IdPicture { get; set; }

        /// <summary>
        /// Tên ảnh
        /// </summary>
        [Required]
        public string NamePicture { get; set; }

        /// <summary>
        /// Đuôi ảnh
        /// </summary>
        [Required]
        public string TailPicture { get; set; }

        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        [Required]
        public string DictionaryPicture { get; set; }

        /// <summary>
        /// Mô tả ảnh
        /// </summary>
        public string DescriptionPicture { get; set; }
    }
}
