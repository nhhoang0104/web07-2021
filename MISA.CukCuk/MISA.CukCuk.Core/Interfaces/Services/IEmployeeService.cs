using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IEmployeeService
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
        ServiceResult Get(string employeeFilter, Guid? departmentId, Guid? positionId, Int32 pageSize, Int32 pageIndex);

        /// <summary>
        /// Lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="id">id nhân viên</param>
        /// <returns>thông tin nhân viên</returns>
        ServiceResult GetById(Guid id);

        /// <summary>
        /// Thêm nhân viên
        /// </summary>
        /// <param name="employee">thông tin nhân viên</param>
        /// <returns></returns>
        ServiceResult Add(Employee employee);

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="id">id nhân viên</param>
        /// <param name="employee">thông tin nhân viên</param>
        /// <returns></returns>
        ServiceResult Update(Guid id, Employee employee);

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="id">id nhân viên</param>
        /// <returns></returns>
        ServiceResult Delete(Guid id);
    }
}
