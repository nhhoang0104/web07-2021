using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        List<Department> Get();

        Department GetById(Guid id);

        Int32 Add(Department department);

        Int32 Update(Guid id, Department department);

        Int32 Delete(Guid id);
    }
}
