using Microsoft.AspNetCore.Http;
using MISA.Core.Enitites;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region Declare
        private readonly ICustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public CustomerService(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository) : base(baseRepository)
        {
            this._customerRepository = customerRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Dịch vụ nhập dữ liệu
        /// </summary>
        /// <param name="file">File dữ liệu</param>
        /// <returns></returns>
        public ServiceResult ImportCustomer(IFormFile file)
        {
            
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.POST;
            try
            {
                if (file == null || file.Length <= 0)
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(Properties.Resource.Emty_Msg);
                    serviceResult.UserMessage = Properties.Resource.User_Empty_Msg;
                    return serviceResult;
                }

                if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(Properties.Resource.Format_Error_Msg);
                    serviceResult.UserMessage = Properties.Resource.Format_Error_Msg;
                    return serviceResult;
                }

                HashSet<string> contain = new HashSet<string>();
                var data = new List<Tuple<Customer, List<string>>>();

                using (var stream = new MemoryStream())
                {
                    file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        
                        for (int row = 3; row <= rowCount; row++)
                        {
                            // chuyển hàng excel => customer
                            var customer = new Customer()
                            {
                                CustomerCode = worksheet.Cells[row, 1].Value.ToString(),
                                Name = worksheet.Cells[row, 2].Value == null ? "" : worksheet.Cells[row, 2].Value.ToString(),
                                GroupCustomer = worksheet.Cells[row, 4].Value == null ? "" : worksheet.Cells[row, 4].Value.ToString(),
                                PhoneNumber = worksheet.Cells[row, 5].Value == null ? "" : worksheet.Cells[row, 5].Value.ToString(),
                                DateOfBirth = "",
                                CompanyName = "",
                                TaxCode = "",
                                Email = worksheet.Cells[row, 9].Value == null ? "" : worksheet.Cells[row, 9].Value.ToString(),
                                Address = "",
                                Note = ""

                            };
                            // kiểm tra trùng trong file
                            if (!contain.Contains(customer.CustomerCode) && !contain.Contains(customer.Email) && !contain.Contains(customer.PhoneNumber))
                            {
                                contain.Add(customer.CustomerCode);
                                contain.Add(customer.PhoneNumber);
                                contain.Add(customer.Email);
                                data.Add(Tuple.Create(customer, new List<string>()));
                            }
                            else
                            {
                                List<string> notifications = new List<string>();
                                if (contain.Contains(customer.CustomerCode))
                                {
                                    notifications.Add("Mã nhân viên đã tồn tại trong file");
                                }
                                if (contain.Contains(customer.Email))
                                {
                                    notifications.Add("Email đã tồn tại trong file");
                                }
                                if (contain.Contains(customer.PhoneNumber))
                                {
                                    notifications.Add("SDT đã tồn tại trong file");
                                }
                                data.Add(Tuple.Create(customer, notifications));
                            }

                        }
                        contain.Clear();
                    }
                }
                string codes = "";
                int size = 0;
                foreach (Tuple<Customer, List<string>> item in data)
                {
                    // xử lý check hàng loạt 
                    // gỘP emp-loyeecode => NV101,NV102,...
                    // truyền size 
                    // ......
                    // không hợp lệ trong file => có check thằng không hợp lệ trong db ?

                    if(item.Item2.Count() == 0)
                    {
                        codes += item.Item1.CustomerCode + ",";
                        size++;
                    }
                    
                }
                codes = codes.Substring(0, codes.Length - 1);
                IEnumerable<EmployeeCode> result = _customerRepository.InsertMultiCustomer(codes, size, ",");
                foreach(EmployeeCode item in result)
                {
                    string code = item.code;
                    int duplicate = item.duplicate;
                    if(duplicate == 1)
                    {
                        //.....
                    }
                }
                
                // bảng giá trị
                // for bảng 



                serviceResult.Data = data;
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
                serviceResult.UserMessage = Properties.Resource.User_Error_Msg;
            }
            return serviceResult;
        }

        /// <summary>
        /// Dịch vụ xuất dữ liệu
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ServiceResult ExportCustomer()
        {
            ServiceResult serviceResult = new ServiceResult();
            
            var customers = new List<Customer>();
            try
            {
                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Cells.LoadFromCollection(customers, true);
                    package.Save();
                }
                serviceResult.Data = stream;
                
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }

        #endregion
    }
}
