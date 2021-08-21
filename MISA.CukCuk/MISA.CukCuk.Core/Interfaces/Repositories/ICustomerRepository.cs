using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface ICustomerRepository:IBaseRepository<Customer>
    {

        /// <summary>
        /// Lay danh sach khach hang theo bo loc va phan trang
        /// </summary>
        /// <param name="customerFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Object GetByFilterPaging(string customerFilter, Int32 pageSize, Int32 pageIndex);

        /// <summary>
        /// Kiểm tra mã khách hàng trùng
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        bool CheckCustomerCodeExists(string customerCode);

        /// <summary>
        /// Kiểm tra số điện thoại
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        bool CheckCustomerPhoneNumberExists(string phoneNumber);

        /// <summary>
        /// Kiểm tra trên nhóm khách hàng
        /// </summary>
        /// <param name="customerGroupName"></param>
        /// <returns></returns>
        Guid? CheckCustomerGroupNameExists(string customerGroupName);

        /// <summary>
        /// Lay tat ca MKH
        /// </summary>
        /// <returns></returns>
        List<string> GetAllCustomerCode();

        /// <summary>
        /// Lay tat ca sdt
        /// </summary>
        /// <returns></returns>
        List<string> GetAllPhoneNumber();
    }
}
