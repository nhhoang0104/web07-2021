using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IPositionService
    {
        ServiceResult Get();
        ServiceResult GetById(Guid id);
        ServiceResult Add(Position position);
        ServiceResult Update(Guid id, Position position);
        ServiceResult Delete(Guid id);
    }
}
