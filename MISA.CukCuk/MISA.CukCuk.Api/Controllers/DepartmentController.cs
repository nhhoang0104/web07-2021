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
    public class DepartmentController : BaseController<Department>
    {
        IDepartmentService _departmentService;

        public DepartmentController(IBaseService<Department> baseService, IDepartmentService departmentService) : base(baseService)
        {
            this._departmentService = departmentService;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var serviceResult = this._departmentService.GetAll();

        //        if (serviceResult.IsValid == true)
        //        {
        //            return StatusCode(200, serviceResult.Data);
        //        }
        //        else
        //        {
        //            return BadRequest(serviceResult);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var errObj = new
        //        {
        //            devMsg = e.Message,
        //            userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
        //            errorCode = "misa-001",
        //            moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
        //            traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
        //        };

        //        return StatusCode(500, errObj);
        //    }
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById(string id)
        //{
        //    try
        //    {
        //        var serviceResult = this._departmentService.GetById(id);

        //        if (serviceResult.IsValid == true)
        //        {
        //            return StatusCode(200, serviceResult.Data);
        //        }
        //        else
        //        {
        //            return BadRequest(serviceResult.Data);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var errObj = new
        //        {
        //            devMsg = e.Message,
        //            userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
        //            errorCode = "misa-001",
        //            moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
        //            traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
        //        };

        //        return StatusCode(500, errObj);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id)
        //{
        //    try
        //    {
        //        var serviceResult = this._departmentService.Delete(id);

        //        if (serviceResult.IsValid == true)
        //        {
        //            return StatusCode(200, serviceResult.Data);
        //        }
        //        else
        //        {
        //            return BadRequest(serviceResult.Data);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var errObj = new
        //        {
        //            devMsg = e.Message,
        //            userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
        //            errorCode = "misa-001",
        //            moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
        //            traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
        //        };

        //        return StatusCode(500, errObj);
        //    }
        //}
    }
}
