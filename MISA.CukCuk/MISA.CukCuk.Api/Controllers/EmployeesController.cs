using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public IActionResult getEmployees()
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var sqlCommand = "SELECT * FROM Employee";
            var customers = dbConnection.Query<Employee>(sqlCommand);

            var response = StatusCode(200, customers);
            return response;
        }

        [HttpGet("{employeeId}")]
        public IActionResult getEmployeeById(Guid employeeId)
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @EmployeeIdParam";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeIdParam", employeeId);

            var employee = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, parameters);

            var response = StatusCode(200, employee);
            return response;
        }

        [HttpPost]

        public IActionResult insertCustomer(Employee employee) 
        {
            // Truy cập vào database:
            // 1. Khai báo thông tin kết nối database:
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối database:
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Khai báo Dynamic Parameters
            var parammeters = new DynamicParameters();

            // 4. Thêm dữ liệu vào trong database:
            var columnName = string.Empty;
            var columnParam = string.Empty;

            // Khởi khóa chính cho nhân viên mới

            employee.EmployeeId = Guid.NewGuid();

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

            var rowEfffect = dbConnection.Execute(sqlCommand, parammeters);

            var response = StatusCode(200, rowEfffect);
            return response;
        }
    }
}
