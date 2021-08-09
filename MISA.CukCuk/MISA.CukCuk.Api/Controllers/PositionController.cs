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
    [Route("api/Position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        [HttpGet]
        public IActionResult getAll()
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var sqlCommand = "SELECT * FROM Position";
            var positions = dbConnection.Query<Position>(sqlCommand);

            var response = StatusCode(200, positions);
            return response;
        }

        [HttpGet("{positionId}")]
        public IActionResult getById(Guid positionId)
        {
            var connectionString = "Host = 47.241.69.179;" +
               "Database = MISA.CukCuk_Demo_NVMANH;" +
               "User Id = dev;" +
               "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var sqlCommand = "SELECT * FROM Position WHERE PositionId = @PositionIdParam";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PositionIdParam", positionId);

            var position = dbConnection.QueryFirstOrDefault<Position>(sqlCommand, parameter);

            var response = StatusCode(200, position);
            return response;
        }
    }
}
