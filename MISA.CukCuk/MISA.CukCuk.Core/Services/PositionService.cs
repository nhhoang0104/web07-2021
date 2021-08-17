using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class PositionService : BaseService<Position>, IPositionService
    {
        public PositionService(IBaseRepository<Position> baseRepository) : base(baseRepository) { }
    }
}
