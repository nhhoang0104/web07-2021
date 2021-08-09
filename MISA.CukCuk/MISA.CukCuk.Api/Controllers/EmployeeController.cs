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
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult getEmployees()
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_NHHoang;" +
                "User Id = dev;" +
                "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var sqlCommand = "SELECT * FROM Employee";
            var employees = dbConnection.Query<Employee>(sqlCommand);

            var response = StatusCode(200, employees);
            return response;
        }


        [HttpGet("paging")]
        public IActionResult paging([FromQuery]int pageSize, [FromQuery]int pageIndex)
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_NHHoang;" +
                "User Id = dev;" +
                "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            DynamicParameters parameters = new DynamicParameters();

            // Tính offset tương đương với việc tính phần tử đầu tiên của trang pageIndex với pageSize.
            parameters.Add("@OffsetParam", (pageIndex - 1) * pageSize);
            parameters.Add("@LimitParam", pageSize);

            var sqlCommand = "SELECT * FROM Employee LIMIT @LimitParam OFFSET @OffsetParam";
            var employees = dbConnection.Query<Employee>(sqlCommand, parameters);

            var response = StatusCode(200, employees);
            return response;
        }

        [HttpGet("filter")]
        public IActionResult filter([FromQuery]string specs, [FromQuery]Guid? departmentId, [FromQuery]Guid? positionId)
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_NHHoang;" +
                "User Id = dev;" +
                "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            DynamicParameters parameters = new DynamicParameters();

            var tmp = string.Empty;
            if(!(specs == null || specs == ""))
            {
                tmp = specs;
            }

            // Gán các giá trị specs( EmployeeCode, PhoneNumber, FullName), DepartmentId, PositionId
            parameters.Add("@EmployeeCode", tmp);
            parameters.Add("@PhoneNumber", tmp);
            parameters.Add("@FullName", tmp);
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@PositionId", positionId);

            var employees = dbConnection.Query<Employee>("Proc_Filter", parameters, commandType:CommandType.StoredProcedure);

            var response = StatusCode(200, employees);
            return response;
        }

        [HttpGet("{employeeId}")]
        public IActionResult getEmployeeById(Guid employeeId)
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_NHHoang;" +
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
        public IActionResult insertEmployee(Employee employee)
        {
            // Truy cập vào database:
            // 1. Khai báo thông tin kết nối database:
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_NHHoang;" +
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

        [HttpPut("{employeeId}")]
        public IActionResult updateEmployee(Guid employeeId, Employee employee)
        {
            // Truy cập vào database:
            // 1. Khai báo thông tin kết nối database:
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_NHHoang;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối database:
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Khai báo Dynamic Parameters
            var parammeters = new DynamicParameters();

            // 4. Thêm dữ liệu vào trong database:
            var column = string.Empty;

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

                //Thêm param tương ứng với mỗi prop của đối tượng:
                if (propName != "EmployeeId" && propName != "EmployeeCode")
                {
                    parammeters.Add($"@{propName}", propValue);

                    column += $"{propName} = @{propName},";
                }
            }

            parammeters.Add($"@EmployeeIdParam", employeeId);
            column = column.Remove(column.Length - 1, 1);


            // 5. Truy vấn database
            var sqlCommand = $"UPDATE Employee SET {column} WHERE EmployeeId = @EmployeeIdParam";

            var rowEfffect = dbConnection.Execute(sqlCommand, parammeters);

            // 6. Phản hồi
            var response = StatusCode(200, rowEfffect);
            return response;
        }
    }
}
