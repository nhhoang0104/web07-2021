using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IDepartmentService
    {
        ServiceResult Get();
        ServiceResult GetById(Guid id);
        ServiceResult Add(Department department);
        ServiceResult Update(Guid id, Department department);
        ServiceResult Delete(Guid id);
    }
}
