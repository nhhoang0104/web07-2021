using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> Get(string employeeFilter, Guid? departmentId, Guid? positionId, Int32 pageSize, Int32 pageIndex);

        Employee GetById(Guid id);

        Int32 Add(Employee employee);

        Int32 Update(Guid id, Employee employee);

        Int32 Delete(Guid id);
    }
}
