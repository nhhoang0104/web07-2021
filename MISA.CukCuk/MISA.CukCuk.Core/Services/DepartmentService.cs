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
    class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;
        private ServiceResult _serviceResult;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
            this._serviceResult = new ServiceResult();
        }

        public ServiceResult Add(Department department)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Delete(Guid id)
        {
            this._serviceResult.Data = this._departmentRepository.Delete(id);

            return this._serviceResult;
        }

        public ServiceResult Get()
        {
            this._serviceResult.Data = this._departmentRepository.Get();

            return this._serviceResult;
        }

        public ServiceResult GetById(Guid id)
        {
            this._serviceResult.Data = this._departmentRepository.GetById(id);

            return this._serviceResult;
        }

        public ServiceResult Update(Guid id, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
