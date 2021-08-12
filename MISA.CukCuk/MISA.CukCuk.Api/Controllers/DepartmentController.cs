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
    public class DepartmentController : ControllerBase
    {

        private IDbConnection _dbConnection;
        /// <summary>
        /// Connect database
        /// </summary>
        /// <param name="configuration"></param>
        public DepartmentController(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("sqlConnection"));
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu phòng ban
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getAll()
        {
            try
            {
                var sqlCommand = "SELECT * FROM Department";
                var departments = this._dbConnection.Query<Department>(sqlCommand);

                if (departments.Count() > 0)
                {
                    return StatusCode(200, departments);
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
        /// Lấy phòng ban theo id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult getById(Guid id)
        {
            try
            {
                var sqlCommand = "SELECT * FROM Department WHERE DepartmentId = @DepartmentIdParam";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@DepartmentIdParam", id);

                var department = this._dbConnection.QueryFirstOrDefault<Department>(sqlCommand, parameter);

                if (department != null) return StatusCode(200, department);
                return NoContent();
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
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="department">dũ  liệu phòng ban</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult insert(Department department)
        {
            try
            {
                // 3. Khai báo Dynamic Parameters
                var parameters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var columnName = string.Empty;
                var columnParam = string.Empty;

                // Khởi khóa chính cho nhân viên mới

                department.DepartmentId = Guid.NewGuid();

                // Đọc từng properties:
                var props = department.GetType().GetProperties();

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(department);

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
                var sqlCommand = $"INSERT INTO Department({columnName}) VALUES({columnParam})";

                var rowEfffect = this._dbConnection.Execute(sqlCommand, parameters);

                // 6. Phản hồi
                if (rowEfffect > 1)
                {
                    return StatusCode(201, rowEfffect);
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


        /// <summary>
        /// Cập nhật dữ liệu phòng ban theo id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <param name="department">dữ liệu phòng ban</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult update(Guid id, Department department)
        {
            try
            {
                // 3. Khai báo Dynamic Parameters
                DynamicParameters parameters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var column = string.Empty;

                // Đọc từng properties:
                var props = department.GetType().GetProperties();

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(department);

                    // Lấy kiểu dữ liệu của prop:
                    var propType = prop.GetType();

                    // Thêm param tương ứng với mỗi prop của đối tượng:
                    if (propValue != null && propName != "DepartmentCode" && propName != "DepartmentId")
                    {
                        parameters.Add($"@{propName}", propValue);

                        column += $"{propName} = @{propName},";
                    }

                }

                parameters.Add($"@DepartmentIdParam", id);
                column = column.Remove(column.Length - 1, 1);


                // 5. Truy vấn database
                var sqlCommand = $"UPDATE Department SET {column} WHERE DepartmentId = @DepartmentIdParam";

                var rowEfffect = this._dbConnection.Execute(sqlCommand, parameters);

                // 6. Phản hồi
                if (rowEfffect > 1)
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
        /// xóa dữ liệu phòng ban theo id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult delete(Guid id)
        {
            try
            {
                // 3. Truy vấn database
                var sqlCommand = "DELETE FROM Department WHERE DepartmentId = @DepartmentIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DepartmentIdParam", id);

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
