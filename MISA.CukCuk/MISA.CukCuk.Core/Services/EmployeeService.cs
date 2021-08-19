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
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeResponsitory) : base(baseRepository)
        {
            this._employeeRepository = employeeResponsitory;
        }

        public ServiceResult CheckEmployeeCodeExists(string employeeCode)
        {

            if (employeeCode == null || employeeCode == "")
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.EmployeeCode_ErrorMsg;

            };

            this._serviceResult.Data = this._employeeRepository.CheckEmployeeCodeExists(employeeCode);

            return this._serviceResult;
        }

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

        public ServiceResult GetNewEmployeeCode()
        {
            var lastEmployeeCode = this._employeeRepository.GetLastEmployeeCode();

            string prefix = String.Empty;
            string code = String.Empty;
            for (var i = 0; i < lastEmployeeCode.Length; i++)
            {
                if (Char.IsDigit(lastEmployeeCode[i])) code += lastEmployeeCode[i];
                else prefix += lastEmployeeCode[i];
            }

            string newEmploeeCode = prefix + (Int64.Parse(code) + 1).ToString();
            this._serviceResult.Data = newEmploeeCode;

            return this._serviceResult;
        }

        /// <summary>
        /// validate data
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        protected override bool ValidateCustom(Employee employee)
        {
            if (this._employeeRepository.CheckEmployeeCodeExists(employee.EmployeeCode))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.EmployeeCodeExists_ErrMsg;

                return false;
            }

            if (!CommonValidate.ValidateEmail(employee.Email))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.Email_ErrorMsg;

                return false;
            }

            return base.ValidateCustom(employee);
        }

    }
}
