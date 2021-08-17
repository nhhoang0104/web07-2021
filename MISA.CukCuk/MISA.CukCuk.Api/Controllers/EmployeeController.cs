using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeeController(IBaseService<Employee> baseService,IEmployeeService employeeService):base(baseService)
        {
            this._employeeService = employeeService;
        }

        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <param name="employeeFilter">thông tin filter(EmployeeCode hoặc PhoneNumber hoặc FullName</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">kích cỡ trang</param>
        /// <param name="pageIndex">index trang</param>
        /// <returns>danh sách nhân viên</returns>
        [HttpGet("Filter")]
        public IActionResult GetByFilterPaging([FromQuery] string employeeFilter, [FromQuery] Guid? departmentId, [FromQuery] Guid? positionId, [FromQuery] int pageSize, [FromQuery] int pageIndex)
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
    
    }
}
