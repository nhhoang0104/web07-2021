using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        ServiceResult GetAll();
        ServiceResult GetById(string id);
        ServiceResult Add(MISAEntity entity);
        ServiceResult Update(string id, MISAEntity entity);
        ServiceResult Delete(string id);
    }
}
