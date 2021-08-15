using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IPositionRepository
    {
        List<Position> Get();

        Position GetById(Guid id);

        Int32 Add(Position position);

        Int32 Update(Guid id, Position position);

        Int32 Delete(Guid id);
    }
}
