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
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeResponsitory) : base(baseRepository)
        {
            this._employeeRepository = employeeResponsitory;
        }

        /// <summary>
        /// Lấy danh sách nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">số phần tử trên trang</param>
        /// <param name="pageIndex">id trang</param>
        /// <returns></returns>
        public ServiceResult GetByFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, int pageSize, int pageIndex)
        {
            var tmp = string.Empty;

            if (!(employeeFilter == null || employeeFilter == ""))
            {
                tmp = employeeFilter;
            };

            this._serviceResult.Data = this._employeeRepository.GetByFilterPaging(tmp, departmentId, positionId, pageSize, pageIndex);
            return this._serviceResult;
        }

        protected override bool ValidateCustom(Employee employee)
        {
            if (!CommonValidate.ValidateCommon(employee.EmployeeCode))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.EmployeeCode_ErrorMsg;

                return false;
            }

            if (this._employeeRepository.CheckEmployeeCodeExists(employee.EmployeeCode))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.EmployeeCodeExists_ErrMsg;

                return false;
            }

            if (!CommonValidate.ValidateCommon(employee.FullName))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.FullName_ErrorMsg;

                return false;
            }

            if (!CommonValidate.ValidateCommon(employee.PhoneNumber))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.PhoneNumber_ErrorMsg;

                return false;
            }

            if (!CommonValidate.ValidateCommon(employee.Email) && !CommonValidate.ValidateEmail(employee.Email))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.Email_ErrorMsg;

                return false;
            }

            return base.ValidateCustom(employee);
        }

    }
}
