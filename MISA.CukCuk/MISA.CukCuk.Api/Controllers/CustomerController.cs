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
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IActionResult getCustomers()
        {
            // Truy cập vào database:
            // 1. Khai báo thông tin kết nối database:
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối database:
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Truy vấn database
            var sqlCommand = "SELECT * FROM Customer";
            var customers = dbConnection.Query<Customer>(sqlCommand);

            // 3. Truy vấn database
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpGet("{customerId}")]
        public IActionResult getById(Guid customerId)
        {
            // Truy cập vào database:
            // 1. Khai báo thông tin kết nối database:
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối database:
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Truy vấn database
            var sqlCommand = "Select * from Customer WHERE CustomerId = @CustomerIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerIdParam", customerId);

            var customers = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand, parameters);

            // 3. Phản hồi
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpPost]
        public IActionResult insertCustomer(Customer customer)
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

            var rowEfffect = dbConnection.Execute(sqlCommand, parameters);

            // 6. Phản hồi
            var response = StatusCode(200, rowEfffect);
            return response;
        }

        [HttpPut("{customerId}")]
        public IActionResult updateCustomer(Guid customerId, Customer customer)
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

            var rowEfffect = dbConnection.Execute(sqlCommand, parammeters);

            // 6. Phản hồi
            var response = StatusCode(200, rowEfffect);
            return response;
        }

        [HttpDelete("{customerId}")]
        public IActionResult deleteCustomer(Guid customerId)
        {
            // Truy cập vào database:
            // 1. Khai báo thông tin kết nối database:
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối database:
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Truy vấn database
            var sqlCommand = "DELETE FROM Customer WHERE CustomerId = @CustomerIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerIdParam", customerId);

            var rowEffect = dbConnection.Execute(sqlCommand, parameters);

            // 3. Phản hồi
            var response = StatusCode(200, rowEffect);
            return response;
        }
    }
}
