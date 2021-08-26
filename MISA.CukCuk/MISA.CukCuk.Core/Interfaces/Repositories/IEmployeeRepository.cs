using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
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
        /// Kiểm tra các trường thông tin employee xem có tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">mã nhan viên</param>
        /// <param name="identifyNumber">cmtnd hoặc cccd</param>
        /// <param name="phoneNumber">số điện thoại</param>
        /// <param name="email">email</param>
        /// <returns>
        /// true - tồn tại
        /// false - chưa tồn tại
        /// </returns>
        bool CheckInfoEmployeeExists(string employeeCode, string identifyNumber, string phoneNumber, string email);

        /// <summary>
        /// Lấy danh sach mã nhân viên
        /// </summary>
        /// <returns>mã nhân viên</returns>
        List<string> GetAllEmployeeCode();
    }
}
