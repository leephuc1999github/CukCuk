using MISA.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    /// <summary>
    /// Lớp kết quả phản hồi
    /// </summary>
    public class ServiceResult
    {
        #region Property
        /// <summary>
        /// Mã kết quả
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        /// Message trả về cho client
        /// </summary>
        public List<string> DevMessage { get; set; }

        /// <summary>
        /// Thông tin khác
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        #endregion

        #region Constructor
        public ServiceResult()
        {
            this.ResultCode = (int) EnumServiceResult.Success;
            this.DevMessage = new List<string>();
            this.UserMessage = null;
            this.MoreInfo = null;
            this.Data = null;
        }
        #endregion

        #region Method
        /// <summary>
        /// Gán log thực hiện thành công
        /// </summary>
        /// <param name="serviceResult"></param>
        /// <param name="data"></param>
        public void SetSuccess(ServiceResult serviceResult, object data)
        {
            serviceResult.ResultCode = (int)EnumServiceResult.Success;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Success_Msg);
            serviceResult.UserMessage = Properties.Resource.User_Success_Msg;
            serviceResult.Data = data;
        }

        /// <summary>
        /// Gán log thực hiện thất bại
        /// </summary>
        /// <param name="serviceResult"></param>
        public void SetNoContent(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)EnumServiceResult.NoContent;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Error_Msg);
            serviceResult.UserMessage = Properties.Resource.User_Info_Msg;
        }

        /// <summary>
        /// Gán log lỗi máy chủ
        /// </summary>
        /// <param name="serviceResult"></param>
        public void SetInternalServerError(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)EnumServiceResult.InternalServerError;
            serviceResult.UserMessage = Properties.Resource.User_Error_Msg;
        }
        #endregion
    }
}
