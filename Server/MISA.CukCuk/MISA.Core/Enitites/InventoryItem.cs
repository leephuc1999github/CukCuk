using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class InventoryItem : BaseEntity
    {
        /// <summary>
        /// Id món ăn
        /// </summary>
        [Key]
        public Guid IdInventoryItem { get; set; }

        /// <summary>
        /// Mã món ăn
        /// </summary>
        [Required]
        [Duplication]
        public string CodeInventoryItem { get; set; }

        /// <summary>
        /// Tên mớn ăn
        /// </summary>
        [Required]
        public string NameInventoryItem { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        [Required]
        public int Price { get; set; }

        /// <summary>
        /// Giá vốn
        /// </summary>
        public int OriginalPrice { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string DescriptionInventoryItem { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [Required]
        public Guid UnitId { get; set; }

        /// <summary>
        /// Nhóm thực đơn
        /// </summary>
        public Guid? InventoryItemGroupId { get; set; }

        /// <summary>
        /// Chế biến tại
        /// </summary>
        public string ProcessingIn { get; set; }

        /// <summary>
        /// Hiển thị lên ds
        /// </summary>
        public int Active { get; set; }

        /// <summary>
        /// Ảnh món ăn
        /// </summary>
        public Guid? PictureId { get; set; }

        /// <summary>
        /// Thay đổi theo thời giá
        /// </summary>
        public int IsChangeByTime { get; set; }

        /// <summary>
        /// Loại món ăn
        /// </summary>
        public string TypeInventoryItem { get; set; }

        /// <summary>
        /// Điều chỉnh giá tự do
        /// </summary>
        public int IsAutoChange { get; set; }

        /// <summary>
        /// xác định nguyên vật liệu
        /// </summary>
        public string HasMaterial { get; set; }

        /// <summary>
        /// Ngừng bán
        /// </summary>
        public int StoppedSell { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [NotMapped]
        public string NameUnit_FULLTEXT { get; set; }

        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        [NotMapped]
        public string NameInventoryItemGroup_FULLTEXT { get; set; }

        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        [NotMapped]
        public string UrlPicture { get; set; }

    }
}
