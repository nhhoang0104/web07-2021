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
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAll()
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var sqlCommand = "SELECT * FROM Department";
            var departments = dbConnection.Query<Department>(sqlCommand);

            var response = StatusCode(200, departments);
            return response;
        }

        [HttpGet("{id}")]
        public IActionResult getById(Guid id)
        {
            var connectionString = "Host = 47.241.69.179;" +
               "Database = MISA.CukCuk_Demo_NVMANH;" +
               "User Id = dev;" +
               "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var sqlCommand = "SELECT * FROM Department WHERE DepartmentId = @DepartmentIdParam";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@DepartmentIdParam", id);

            var department = dbConnection.QueryFirstOrDefault<Department>(sqlCommand, parameter);

            var response = StatusCode(200, department);
            return response;
        }
    }
}
