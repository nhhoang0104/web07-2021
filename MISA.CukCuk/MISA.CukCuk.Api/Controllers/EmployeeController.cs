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
    public class EmployeeController : ControllerBase
    {

        private IDbConnection _dbConnection;

        /// <summary>
        /// Connect database
        /// </summary>
        /// <param name="configuration"></param>
        public EmployeeController(IConfiguration configuration)
        {

            _dbConnection = new MySqlConnection(configuration.GetConnectionString("sqlConnection"));
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        [HttpGet]
        public IActionResult getAll()
        {
            try
            {
                var sqlCommand = "SELECT * FROM Employee";
                var employees = this._dbConnection.Query<Employee>(sqlCommand);

                if (employees.Count() > 0)
                {
                    return StatusCode(200, employees);

                }
                else
                {
                    return StatusCode(204, employees);
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
        /// Phân trang
        /// </summary>
        /// <param name="pageSize">kích thích trang</param>
        /// <param name="pageIndex">số trang</param>
        /// <returns>Danh sách nhân viên</returns>
        [HttpGet("Paging")]
        public IActionResult paging([FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                // Tính offset tương đương với việc tính phần tử đầu tiên của trang pageIndex với pageSize.
                parameters.Add("@OffsetParam", (pageIndex - 1) * pageSize);
                parameters.Add("@LimitParam", pageSize);

                var sqlCommand = "SELECT * FROM Employee LIMIT @LimitParam OFFSET @OffsetParam";
                var employees = this._dbConnection.Query<Employee>(sqlCommand, parameters);

                if (employees.Count() > 0)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return StatusCode(204, employees);

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
        /// Lọc dũ liệu
        /// </summary>
        /// <param name="specs">EmployeeCode</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="positionId">Id position</param>
        /// <returns>danh sách nhân viên</returns>
        [HttpGet("Filter")]
        public IActionResult filter([FromQuery] string employeeFilter, [FromQuery] Guid? departmentId, [FromQuery] Guid? positionId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                var tmp = string.Empty;
                if (!(employeeFilter == null || employeeFilter == ""))
                {
                    tmp = employeeFilter;
                }

                // Gán các giá trị EmployeeFilter( EmployeeCode, PhoneNumber, FullName), DepartmentId, PositionId
                parameters.Add("@EmployeeFilter", tmp);
                parameters.Add("@DepartmentId", departmentId);
                parameters.Add("@PositionId", positionId);

                var employees = this._dbConnection.Query<Employee>("Proc_GetEmployeeFilter", parameters, commandType: CommandType.StoredProcedure);

                if (employees.Count() > 0)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return StatusCode(204, employees);
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
        /// Lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="employeeId">id nhân viên</param>
        /// <returns>Thông tin nhân viên</returns>
        [HttpGet("{employeeId}")]
        public IActionResult getById(Guid employeeId)
        {
            try
            {
                var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeIdParam";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeIdParam", employeeId);

                var employee = this._dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, parameters);

                if (employee != null)
                {
                    return StatusCode(200, employee);
                }
                else
                {
                    return StatusCode(204, employee);
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
        /// Thêm nhân viên
        /// </summary>
        /// <param name="employee">thông tin nhân viên</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult insert(Employee employee)
        {
            try
            {
                // 3. Khai báo Dynamic Parameters
                var parammeters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var columnName = string.Empty;
                var columnParam = string.Empty;

                // Khởi khóa chính cho nhân viên mới

                employee.EmployeeId = Guid.NewGuid();
                employee.CreatedBy = "NHHoang";
                employee.CreatedDate = DateTime.Now;

                // Đọc từng properties:
                var props = employee.GetType().GetProperties();

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(employee);

                    // Lấy kiểu dữ liệu của prop:
                    var propType = prop.GetType();

                    // Thêm param tương ứng với mỗi prop của đối tượng:
                    parammeters.Add($"@{propName}", propValue);

                    columnName += $"{propName},";
                    columnParam += $"@{propName},";
                }

                columnName = columnName.Remove(columnName.Length - 1, 1);
                columnParam = columnParam.Remove(columnParam.Length - 1, 1);

                var sqlCommand = $"INSERT INTO Employee({columnName}) VALUES({columnParam})";

                var rowEfffect = this._dbConnection.Execute(sqlCommand, parammeters);

                if (rowEfffect > 0)
                {
                    return StatusCode(201, rowEfffect);
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
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employeeId">id nhân viên</param>
        /// <param name="employee">thông tin nhân viên cập nhật</param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public IActionResult update(Guid employeeId, Employee employee)
        {
            try
            {
                // 3. Khai báo Dynamic Parameters
                var parammeters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var column = string.Empty;

                // Đọc từng properties:
                var props = employee.GetType().GetProperties();

                employee.ModifiedBy = "NHHoang";
                employee.ModifiedDate = DateTime.Now;

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(employee);

                    // Lấy kiểu dữ liệu của prop:
                    var propType = prop.GetType();

                    //Thêm param tương ứng với mỗi prop của đối tượng:
                    if (propName != "EmployeeId" && propValue != null)
                    {
                        parammeters.Add($"@{propName}", propValue);

                        column += $"{propName} = @{propName},";
                    }
                }

                parammeters.Add($"@EmployeeIdParam", employeeId);
                column = column.Remove(column.Length - 1, 1);


                // 5. Truy vấn database
                var sqlCommand = $"UPDATE Employee SET {column} WHERE EmployeeId = @EmployeeIdParam";

                var rowEfffect = this._dbConnection.Execute(sqlCommand, parammeters);

                // 6. Phản hồi
                if (rowEfffect > 0) return StatusCode(200, rowEfffect);
                else
                {
                    return NoContent();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="id">id nhân viên</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult delete(Guid id)
        {
            try
            {
                // 3. Truy vấn database
                var sqlCommand = "DELETE FROM Employee WHERE EmployeeId = @EmployeeIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeIdParam", id);

                var rowEffect = this._dbConnection.Execute(sqlCommand, parameters);

                // 3. Phản hồi
                if (rowEffect > 1)
                {
                    return StatusCode(200, rowEffect);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        ///// <summary>
        ///// Lấy mã nhân viên mới
        ///// </summary>
        ///// <returns>mã nhân viên mới</returns>
        //[HttpGet("NewEmployeeCode")]
        //public IActionResult getNewEmployeeCode()
        //{
        //    var code = "NV-12313";
        //    // 3. Phản hồi
        //    var response = StatusCode(200, code);
        //    return response;
        //}
    }
}
