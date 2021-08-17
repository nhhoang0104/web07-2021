using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IBaseRepository<MISAEntity>
    {
        List<MISAEntity> GetAll();

        MISAEntity GetById(Guid id);

        Int32 Add(MISAEntity entity);

        Int32 Update(Guid id, MISAEntity entity);

        Int32 Delete(Guid id);
    }
}
