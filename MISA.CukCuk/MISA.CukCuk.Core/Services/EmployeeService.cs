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

            this._serviceResult.Data = this._employeeRepository.CheckInfoEmployeeExists(employeeCode, null, null, null);

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

        /// <summary>
        /// Laasy ma nhan vien moi
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetNewEmployeeCode()
        {
            var employeeCodeList = this._employeeRepository.GetAllEmployeeCode();
            var lastEmployeeCode = string.Empty;

            if (employeeCodeList.Count == 0)
            {
                this._serviceResult.Data = "NV-001";
                return this._serviceResult;
            }
            else
            {
                employeeCodeList.Sort(delegate (string a, string b)
                {
                    if (b.Length == a.Length) return b.CompareTo(a);
                    else return b.Length - a.Length;
                });

                lastEmployeeCode = employeeCodeList[0];
            }


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
        /// validate cac trường dữ liệu đặc biệt(EmployeeCode, PhoneNumber, IdentifyNumber, Email)
        /// </summary>
        /// <param name="employee">thông tin nhân viên</param>
        /// <returns></returns>
        protected override bool ValidateCustom(Employee employee)
        {
            if (this._employeeRepository.CheckInfoEmployeeExists(null, employee.IdentityNumber, null, null))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.IdentifyNumberExsist_ErrorMsg;

                return false;
            }

            if (this._employeeRepository.CheckInfoEmployeeExists(null, null, employee.PhoneNumber, null))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.PhoneNumberExsist_ErrorMsg;

                return false;
            }

            if (!CommonValidate.ValidateEmail(employee.Email))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.Email_ErrorMsg;

                return false;
            }

            if (this._employeeRepository.CheckInfoEmployeeExists(null, null, null, employee.Email))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.Email_ErrorMsg;

                return false;
            }

            return base.ValidateCustom(employee);
        }

        protected override bool ValidateEntityCode(Employee employee)
        {
            if (this._employeeRepository.CheckInfoEmployeeExists(employee.EmployeeCode, null, null, null))
            {
                this._serviceResult.IsValid = false;
                this._serviceResult.Messager = Resources.ErrorMessage.EmployeeCodeExists_ErrMsg;

                return false;
            }

            return base.ValidateEntityCode(employee);
        }
    }
}
