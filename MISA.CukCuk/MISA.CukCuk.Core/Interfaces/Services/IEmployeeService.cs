using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <param name="employeeFilter">thông tin filter(EmployeeCode hoặc PhoneNumber hoặc FullName)</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">kích cỡ tragn</param>
        /// <param name="pageIndex">id trang</param>
        /// <returns></returns>
        ServiceResult GetByFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, Int32 pageSize, Int32 pageIndex);

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại chưa ?
        /// </summary>
        /// <param name="EmployeeCode">mã nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - chưa
        /// </returns>
        ServiceResult CheckEmployeeCodeExists(string EmployeeCode);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        ServiceResult GetNewEmployeeCode();
    }
}
