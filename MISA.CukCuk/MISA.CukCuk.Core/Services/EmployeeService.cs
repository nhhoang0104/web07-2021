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
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private ServiceResult _serviceResult;
        public EmployeeService(IEmployeeRepository employeeResponsitory)
        {
            this._employeeRepository = employeeResponsitory;
            this._serviceResult = new ServiceResult();
        }

        public ServiceResult Get(string employeeFilter, Guid? departmentId, Guid? positionId, int pageSize, int pageIndex)
        {
            var tmp = string.Empty;

            if (!(employeeFilter == null || employeeFilter == ""))
            {
                tmp = employeeFilter;
            };

            this._serviceResult.Data = this._employeeRepository.Get(tmp, departmentId, positionId, pageSize, pageIndex);
            return this._serviceResult;
        }

        public ServiceResult GetById(Guid id)
        {
            this._serviceResult.Data = this._employeeRepository.GetById(id);

            return this._serviceResult;
        }

        public ServiceResult Add(Employee employee)
        {
            this._serviceResult.Data = this._employeeRepository.Add(employee);

            return this._serviceResult;
        }

        public ServiceResult Update(Guid id, Employee employee)
        {
            this._serviceResult.Data = this._employeeRepository.Update(id, employee);
            return this._serviceResult;
        }

        public ServiceResult Delete(Guid id)
        {
            this._serviceResult.Data = this._employeeRepository.Delete(id);
            return this._serviceResult;
        }
    }

}
