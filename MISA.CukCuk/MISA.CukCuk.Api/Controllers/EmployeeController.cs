using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entities;
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
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            this._employeeService = employeeService;
        }

        /// <summary>
        /// Lấy dữ liệu theo bộ lộc và phân trang
        /// </summary>
        /// <param name="employeeFilter">thông tin filter(EmployeeCode hoặc PhoneNumber hoặc FullName</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">kích cỡ trang</param>
        /// <param name="pageIndex">index trang</param>
        /// <returns>danh sách nhân viên</returns>
        [HttpGet("Filter")]
        public IActionResult GetByFilterPaging([FromQuery] string? employeeFilter, [FromQuery] Guid? departmentId, [FromQuery] Guid? positionId, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {
                var serviceResult = _employeeService.GetByFilterPaging(employeeFilter, departmentId, positionId, pageSize, pageIndex);

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
        /// Kiểm tra mã nhân viên đã tồn tại chưa.
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns></returns>
        [HttpGet("CheckEmployeeExists/{employeeCode}")]
        public IActionResult CheckEmployeeExists(string employeeCode)
        {
            try
            {
                var serviceResult = this._employeeService.CheckEmployeeCodeExists(employeeCode);
                if (!serviceResult.IsValid)
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                        errorCode = "misa-001",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };

                    return BadRequest(errObj);
                }

                return StatusCode(200, serviceResult.Data);
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
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var serviceResult = this._employeeService.GetNewEmployeeCode();

                return StatusCode(200, serviceResult.Data);
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
    }
}
