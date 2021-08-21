using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Object GetByFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, int pageSize, int pageIndex)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@EmployeeFilter", employeeFilter);
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@PositionId", positionId);
            parameters.Add("@Size", pageSize);
            parameters.Add("@Offset", (pageIndex-1)*pageSize);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var employees = dbConnection.Query<Employee>("Proc_GetEmployeeFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

                var data = new
                {
                    employees = (List<Employee>)employees,
                    totalPage = parameters.Get<Int32>("@TotalPage"),
                    totalRecord = parameters.Get<Int32>("@TotalRecord")
                };

                return data;
            }
        }

        public bool CheckEmployeeCodeExists(string employeeCode)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeCode", employeeCode);
            dynamicParameters.Add("@IsExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckEmployeeCodeExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                bool isExists = dynamicParameters.Get<bool>("@IsExists");
                return isExists;
            }
        }

        public string GetLastEmployeeCode()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var lastEmpoyeeCode = dbConnection.QueryFirstOrDefault<string>("Proc_GetLastEmployeeCode", commandType: CommandType.StoredProcedure);
           
                return lastEmpoyeeCode;
            }
        }

        public bool CheckEmployeePhoneNumberExists(string phoneNumber)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@PhoneNumber", phoneNumber);
            dynamicParameters.Add("@IsExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckEmployeePhoneNumberExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                bool isExists = dynamicParameters.Get<bool>("@IsExists");
                return isExists;
            }
        }

        public bool CheckEmployeeIdentifyNumberExists(string identifyNumber)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@IdentifyNumber", identifyNumber);
            dynamicParameters.Add("@IsExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckEmployeeIdentifyNumberExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                bool isExists = dynamicParameters.Get<bool>("@IsExists");
                return isExists;
            }
        }
    }
}
