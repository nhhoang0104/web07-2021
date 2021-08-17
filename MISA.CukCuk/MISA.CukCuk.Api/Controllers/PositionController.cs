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
    public class PositionController : BaseController<Position>
    {
        IPositionService _positionService;

        public PositionController(IBaseService<Position> baseService,IPositionService positionService):base(baseService)
        {
            this._positionService = positionService;
        }
    }
}
