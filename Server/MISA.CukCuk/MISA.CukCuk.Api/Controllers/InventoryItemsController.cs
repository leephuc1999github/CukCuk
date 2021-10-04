using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InventoryItemsController : BaseController<InventoryItem>
    {
        #region Declare
        private readonly IInventoryItemService _inventoryItemService;
        #endregion

        #region Constructor
        public InventoryItemsController(IBaseService<InventoryItem> baseService, IInventoryItemService inventoryItemService) : base(baseService)
        {
            this._inventoryItemService = inventoryItemService;
        }

        /// <summary>
        /// API Phân trang món ăn
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("Paging")]
        [HttpPost]
        public IActionResult GetPaging([FromBody] List<BaseFilter> filters, int pageIndex, int pageSize)
        {
            var inventoryItems = this._inventoryItemService.GetInventoryItemsPaging(filters, pageIndex, pageSize);
            return Ok(inventoryItems);
        }

        /// <summary>
        /// Dịch vụ thêm mới món ăn
        /// </summary>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public IActionResult Insert([FromForm] IFormFile file, [FromForm] string data, [FromForm] string service)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                InventoryItem inventoryItem = JsonConvert.DeserializeObject<InventoryItem>(data);
                List<ServiceAddition> serviceAdditions = new List<ServiceAddition>();
                if (service != null)
                {
                    serviceAdditions = JsonConvert.DeserializeObject<List<ServiceAddition>>(service);
                }
                Picture picture = new Picture();
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    picture.IdPicture = Guid.NewGuid();
                    picture.NamePicture = picture.IdPicture.ToString();
                    picture.DescriptionPicture = fileName;
                    picture.DictionaryPicture = "images/inventory-item/";
                    picture.TailPicture = Path.GetExtension(file.FileName);
                    inventoryItem.PictureId = picture.IdPicture;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\inventory-item", picture.NamePicture + picture.TailPicture);
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    file.CopyToAsync(fileStream);
                }

                // Trường hợp nhân bản dữ liệu
                if(inventoryItem.PictureId != null && file == null)
                {
                    DirectoryInfo folder = new DirectoryInfo($"{Directory.GetCurrentDirectory()}/wwwroot/images/inventory-item");
                    // tìm ảnh theo picture id
                    FileInfo[] imgs = folder.GetFiles($"{inventoryItem.PictureId}.*");
                    if(imgs.Length > 0)
                    {
                        FileInfo img = imgs[0];
                        picture.IdPicture = Guid.NewGuid();
                        picture.NamePicture = picture.IdPicture.ToString();
                        picture.DictionaryPicture = "images/inventory-item/";
                        picture.TailPicture = img.Extension;
                        string cloneFilePath = $"{img.DirectoryName}\\{inventoryItem.PictureId}{picture.TailPicture}";
                        string newFilePath = $"{img.DirectoryName}\\{picture.NamePicture}{picture.TailPicture}";
                        System.IO.File.Copy(cloneFilePath, newFilePath, true);

                        inventoryItem.PictureId = picture.IdPicture;
                    }
                }
                serviceResult = this._inventoryItemService.InsertInventoryItem(inventoryItem, picture, serviceAdditions);
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetBadRequest(serviceResult);
            }
            return Ok(serviceResult);

        }

        /// <summary>
        /// API cập nhật món ăn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPut("Edit/{id}")]
        public IActionResult Update(Guid id, [FromForm] IFormFile file, [FromForm] string data, [FromForm] string service)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                // Chuyển dữ liệu sang đối tượng
                InventoryItem inventoryItem = JsonConvert.DeserializeObject<InventoryItem>(data);
                // Chuyển dữ liệu dịch vụ sang danh sách
                List<ServiceAddition> serviceAdditions = new List<ServiceAddition>();
                if (service != null)
                {
                    serviceAdditions = JsonConvert.DeserializeObject<List<ServiceAddition>>(service);
                }
                // Tạo đối tượng ảnh
                Picture picture = new Picture();

                string filePath = "";
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    picture.IdPicture = (Guid)(inventoryItem.PictureId != null ? inventoryItem.PictureId : Guid.NewGuid());
                    picture.NamePicture = picture.IdPicture.ToString();
                    picture.DescriptionPicture = fileName;
                    picture.DictionaryPicture = "images/inventory-item/";
                    picture.TailPicture = Path.GetExtension(file.FileName);
                    // Gán ảnh cho món ăn
                    inventoryItem.PictureId = picture.IdPicture;

                    // Clone ảnh
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\inventory-item", picture.NamePicture + picture.TailPicture);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    file.CopyToAsync(fileStream);
                }
                // Gọi dịch vụ cập nhật ảnh
                serviceResult = this._inventoryItemService.UpdateInventoryItem(id, inventoryItem, picture, serviceAdditions);

            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
            }
            return Ok(serviceResult);
        }

        /// <summary>
        /// API sinh mã món ăn
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("NewCode")]
        public IActionResult NewInventoryItemCode(string columnName, string value, Guid id)
        {
            ServiceResult serviceResult = this._inventoryItemService.NewInventoryItemCode(columnName, value, id);
            return Ok(serviceResult);
        }

        #endregion
    }
}
