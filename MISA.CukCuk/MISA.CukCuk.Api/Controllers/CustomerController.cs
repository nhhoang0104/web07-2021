using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Api.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IDbConnection _dbConnection;

        /// <summary>
        /// kết nối database
        /// </summary>
        /// <param name="configuration"></param>
        public CustomerController(IConfiguration configuration)
        {

            _dbConnection = new MySqlConnection(configuration.GetConnectionString("sqlConnection"));
        }

        /// <summary>
        /// Lấy tất cả nhân viên
        /// </summary>
        /// <returns>danh sách nhân viên</returns>
        [HttpGet]
        public IActionResult getAll()
        {
            try
            {
                // 3. Truy vấn database
                var sqlCommand = "SELECT * FROM Customer";
                var customers = this._dbConnection.Query<Customer>(sqlCommand);

                // 3. Truy vấn database
                if (customers.Count() > 0)
                {
                    return StatusCode(200, customers);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Lấy dữ liệu theo phân trang
        /// </summary>
        /// <param name="pageSize">Pha</param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet("Paging")]
        public IActionResult paging([FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                // Tính offset tương đương với việc tính phần tử đầu tiên của trang pageIndex với pageSize.
                parameters.Add("@OffsetParam", (pageIndex - 1) * pageSize);
                parameters.Add("@LimitParam", pageSize);

                var sqlCommand = "SELECT * FROM Customer LIMIT @LimitParam OFFSET @OffsetParam";
                var employees = this._dbConnection.Query<Customer>(sqlCommand, parameters);

                if (employees.Count() > 1)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Lọc nhân viên
        /// </summary>
        /// <param name="customerFilter">thòng tin lọc(mã khách hàng</param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult filter([FromQuery] string customerFilter)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                var tmp = string.Empty;
                if (!(customerFilter == null || customerFilter == ""))
                {
                    tmp = customerFilter;
                }

                // Gán các giá trị customerFilter( CustomerCode, PhoneNumber, FullName)
                parameters.Add("@CustomerFilter", tmp);


                var employees = this._dbConnection.Query<Customer>("Proc_GetCustomerFilter", parameters, commandType: CommandType.StoredProcedure);

                if (employees.Count() > 1)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Lấy thông tin khách hàng theo thông tin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult getById(Guid id)
        {
            try
            {
                // 3. Truy vấn database
                var sqlCommand = "Select * from Customer WHERE CustomerId = @CustomerIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerIdParam", id);

                var customer = this._dbConnection.QueryFirstOrDefault<Customer>(sqlCommand, parameters);

                // 3. Phản hồi
                if (customer != null)
                {
                    return StatusCode(200, customer);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="customer">thông tin khách hàng</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult insert(Customer customer)
        {

            // Validate cac truong
            if (customer.CustomerCode != null || customer.CustomerCode.Trim() != "")
            {
            }


            try
            {
                // 3. Khai báo Dynamic Parameters
                var parameters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var columnName = string.Empty;
                var columnParam = string.Empty;

                // Khởi khóa chính cho nhân viên mới

                customer.CustomerId = Guid.NewGuid();

                // Đọc từng properties:
                var props = customer.GetType().GetProperties();

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(customer);

                    // Lấy kiểu dữ liệu của prop:
                    var propType = prop.GetType();

                    // Thêm param tương ứng với mỗi prop của đối tượng:
                    parameters.Add($"@{propName}", propValue);

                    columnName += $"{propName},";
                    columnParam += $"@{propName},";
                }

                columnName = columnName.Remove(columnName.Length - 1, 1);
                columnParam = columnParam.Remove(columnParam.Length - 1, 1);

                // 5. Truy vấn database
                var sqlCommand = $"INSERT INTO Customer({columnName}) VALUES({columnParam})";

                var rowEfffect = this._dbConnection.Execute(sqlCommand, parameters);

                // 6. Phản hồi
                if (rowEfffect > 0)
                {
                    return StatusCode(200, rowEfffect);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };
                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// cập nhật thông tin khach hàng
        /// </summary>
        /// <param name="customerId">id khách hàng</param>
        /// <param name="customer">thông tin khách hàng</param>
        /// <returns></returns>
        [HttpPut("{customerId}")]
        public IActionResult update(Guid customerId, Customer customer)
        {
            try
            {
                // 3. Khai báo Dynamic Parameters
                var parammeters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var column = string.Empty;

                // Đọc từng properties:
                var props = customer.GetType().GetProperties();

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(customer);

                    // Lấy kiểu dữ liệu của prop:
                    var propType = prop.GetType();

                    //Thêm param tương ứng với mỗi prop của đối tượng:
                    if (propName != "CustomerId" && propName != "CustomerCode" && propValue != null)
                    {
                        parammeters.Add($"@{propName}", propValue);

                        column += $"{propName} = @{propName},";
                    }
                }

                parammeters.Add($"@CustomerIdParam", customerId);
                column = column.Remove(column.Length - 1, 1);


                // 5. Truy vấn database
                var sqlCommand = $"UPDATE Customer SET {column} WHERE CustomerId = @CustomerIdParam";

                var rowEfffect = this._dbConnection.Execute(sqlCommand, parammeters);

                // 6. Phản hồi
                if (rowEfffect > 0)
                {
                    return StatusCode(200, rowEfffect);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="customerId">id khách hàng</param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public IActionResult delete(Guid customerId)
        {
            try
            {
                // 3. Truy vấn database
                var sqlCommand = "DELETE FROM Customer WHERE CustomerId = @CustomerIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerIdParam", customerId);

                var rowEffect = this._dbConnection.Execute(sqlCommand, parameters);

                // 3. Phản hồi
                if (rowEffect > 0)
                {
                    return StatusCode(200, rowEffect);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }

        }
    }
}
