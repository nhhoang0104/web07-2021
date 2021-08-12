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
    public class PositionController : ControllerBase
    {

        private IDbConnection _dbConnection;

        /// <summary>
        /// Connect database
        /// </summary>
        /// <param name="configuration">info database</param>
        public PositionController(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("sqlConnection"));
        }

        /// <summary>
        /// Lấy toàn bộ vị trí
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getAll()
        {
            try
            {
                var sqlCommand = "SELECT * FROM Position";
                var positions = this._dbConnection.Query<Position>(sqlCommand);

                if (positions.Count() > 0)
                {
                    return StatusCode(200, positions);
                }
                else
                {
                    return StatusCode(204, positions);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Lấy vị trí theo id
        /// </summary>
        /// <param name="positionId">id vị trí</param>
        /// <returns></returns>
        [HttpGet("{positionId}")]
        public IActionResult getById(Guid positionId)
        {
            try
            {
                var sqlCommand = "SELECT * FROM Position WHERE PositionId = @PositionIdParam";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@PositionIdParam", positionId);

                var position = this._dbConnection.QueryFirstOrDefault<Position>(sqlCommand, parameter);

                if (position != null)
                {
                    return StatusCode(200, position);
                }
                else
                {
                    return StatusCode(204, position);
                }

            }
            catch (Exception e)
            {

                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// thêm vị trí mới
        /// </summary>
        /// <param name="position">thông tin vị trí mới</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult insert(Position position)
        {
            try
            {
                // 3. Khai báo Dynamic Parameters
                var parameters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var columnName = string.Empty;
                var columnParam = string.Empty;

                // Khởi khóa chính cho nhân viên mới

                position.PositionId = Guid.NewGuid();

                // Đọc từng properties:
                var props = position.GetType().GetProperties();

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(position);

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
                var sqlCommand = $"INSERT INTO Position({columnName}) VALUES({columnParam})";

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
        /// Cập nhật thông tin vị trí
        /// </summary>
        /// <param name="id">id vị trí</param>
        /// <param name="position">thông tin vị trí</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult update(Guid id, Position position)
        {
            try
            {
                // 3. Khai báo Dynamic Parameters
                DynamicParameters parameters = new DynamicParameters();

                // 4. Thêm dữ liệu vào trong database:
                var column = string.Empty;

                // Đọc từng properties:
                var props = position.GetType().GetProperties();

                // Duyệt từng properties:
                foreach (var prop in props)
                {
                    // Lấy tên của prop:
                    var propName = prop.Name;

                    // Lấy giá trị của prop trong đối tượng:
                    var propValue = prop.GetValue(position);

                    // Lấy kiểu dữ liệu của prop:
                    var propType = prop.GetType();

                    // Thêm param tương ứng với mỗi prop của đối tượng:
                    if (propValue != null && propName != "PositionId" && propName != "PositionCode")
                    {
                        parameters.Add($"@{propName}", propValue);

                        column += $"{propName} = @{propName},";
                    }

                }

                parameters.Add($"@PositionIdParam", id);
                column = column.Remove(column.Length - 1, 1);


                // 5. Truy vấn database
                var sqlCommand = $"UPDATE Position SET {column} WHERE PositionId = @PositionIdParam";

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
        /// xóa vị trí
        /// </summary>
        /// <param name="id">id vị trí</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult delete(Guid id)
        {
            try
            {
                // 3. Truy vấn database
                var sqlCommand = "DELETE FROM Position WHERE PositionId = @PositionIdParam";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PositionIdParam", id);

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
