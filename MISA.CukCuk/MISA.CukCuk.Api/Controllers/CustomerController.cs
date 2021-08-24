using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Attributes;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MISA.CukCuk.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    public class CustomerController : BaseController<Customer>
    {
        ICustomerService _customerService;
        IBaseRepository<Customer> _baseRepository;
        public CustomerController(ICustomerService customerService, IBaseRepository<Customer> baseRepository) : base(customerService)
        {
            this._customerService = customerService;
            this._baseRepository = baseRepository;
        }

        [HttpGet("Filter")]
        public IActionResult GetByFilterPaging([FromQuery] string customerFilteruid, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {
                var serviceResult = _customerService.GetAll();

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// import file
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("Import")]
        public IActionResult Import(IFormFile? formFile, CancellationToken cancellationToken)
        {
            try
            {
                if (formFile == null)
                {
                    var errObj = new
                    {
                        devMsg = "File is null",
                        userMsg = "Vui lòng chọn tệp nhập khẩu!",
                    };

                    return StatusCode(400, errObj);
                };

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase) && !Path.GetExtension(formFile.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    var errObj = new
                    {
                        devMsg = "File không đúng định dạng",
                        userMsg = "File không đúng định dạng. Chỉ hỗ trợ file .xls, xlsx",
                    };

                    return StatusCode(400, errObj);
                }

                var serviceResult = this._customerService.Import(formFile, cancellationToken);

                return StatusCode(200, serviceResult.Data);
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        [HttpGet("Export")]

        public async Task<IActionResult> Export(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var customers = new List<Customer>();

            customers = this._baseRepository.GetAll();

            var stream = new MemoryStream();

            var properties = typeof(Customer).GetProperties();



            using (var package = new ExcelPackage(stream))
            {

                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(customers, true);
                var column = 1;

                foreach (var prop in properties)
                {
                    var propMISAExport = prop.GetCustomAttributes(typeof(MISAExport), true);
                    var isHidden = (propMISAExport[0] as MISAExport).isHidden;

                    // xet cac truong export hay ko?
                    if (isHidden)
                    {
                        workSheet.Cells.AutoFitColumns();
                        workSheet.Column(column).Hidden = true;
                    }
                    
                    // dinh dang ngay thang nam
                    if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                    {
                        workSheet.Column(column).Style.Numberformat.Format = "mm/dd/yyyy";
                    }



                    column++;
                }

                package.Save();
            }

            stream.Position = 0;
            string excelName = $"DanhSachThongTinKhachHang.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
