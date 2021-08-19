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
    }
}
