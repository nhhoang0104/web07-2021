using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class PositionService : IPositionService
    {
        private IPositionService _positionService;
        private ServiceResult _serviceResult;

        public ServiceResult Add(Position position)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Delete(Guid id)
        {
            this._serviceResult.Data = this._positionService.Delete(id);

            return this._serviceResult;
        }

        public ServiceResult Get()
        {
            this._serviceResult.Data = this._positionService.Get();

            return this._serviceResult;
        }

        public ServiceResult GetById(Guid id)
        {
            this._serviceResult.Data = this._positionService.GetById(id);

            return this._serviceResult;
        }

        public ServiceResult Update(Guid id, Position position)
        {
            throw new NotImplementedException();
        }
    }
}
