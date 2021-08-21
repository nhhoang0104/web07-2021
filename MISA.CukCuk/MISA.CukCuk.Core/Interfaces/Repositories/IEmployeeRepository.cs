using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy danh sách sách nhân viên theo bộ lọc, phân trang
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns>danh sách nhân viên</returns>
        Object GetByFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, Int32 pageSize, Int32 pageIndex);

        /// <summary>
        /// Kiểm tra mã nhân viên tồn tại
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>
        /// true - trùng
        /// false - ko trùng
        /// </returns>
        bool CheckEmployeeCodeExists(string employeeCode);

        /// <summary>
        /// Kiem tra so dien thoai ton tai
        /// </summary>
        /// <param name="phoneNumber">sdt</param>
        /// <returns></returns>
        bool CheckEmployeePhoneNumberExists(string phoneNumber);

        /// <summary>
        /// Kiem tra cmtnd hoac cccd ton tai
        /// </summary>
        /// <param name="identifyNumber">cmtnd hoac cccd</param>
        /// <returns></returns>
        bool CheckEmployeeIdentifyNumberExists(string identifyNumber);

        /// <summary>
        /// Lấy mã nhân viên mã mới
        /// </summary>
        /// <returns>mã nhân viên</returns>
        string GetLastEmployeeCode();
    }
}
