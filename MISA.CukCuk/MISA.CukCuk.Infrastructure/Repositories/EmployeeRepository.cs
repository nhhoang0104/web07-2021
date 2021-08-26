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
            parameters.Add("@Offset", pageIndex);
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

        public List<string> GetAllEmployeeCode()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var empoyeeCodeList = dbConnection.Query<string>("Proc_GetAllEmployeeCode", commandType: CommandType.StoredProcedure);

                return (List<string>)empoyeeCodeList;
            }
        }

        public bool CheckInfoEmployeeExists(string employeeCode, string identifyNumber, string phoneNumber, string email)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeCode", employeeCode);
            dynamicParameters.Add("@PhoneNumber", phoneNumber);
            dynamicParameters.Add("@Email", email);
            dynamicParameters.Add("@IdentityNumber", identifyNumber);
            dynamicParameters.Add("@IsExists", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckInfoEmployeeExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                Int32 isExists = dynamicParameters.Get<Int32>("@IsExists");
                return isExists == 1 ? true : false;
            }
        }
    }
}
