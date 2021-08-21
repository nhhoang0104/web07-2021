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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool CheckCustomerCodeExists(string customerCode)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerCode", customerCode);
            dynamicParameters.Add("@IsExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckCustomerCodeExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                bool isExists = dynamicParameters.Get<bool>("@IsExists");
                return isExists;
            }
        }

        public Guid? CheckCustomerGroupNameExists(string customerGroupName)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerGroupName", customerGroupName);
            dynamicParameters.Add("@IsExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            dynamicParameters.Add("@CustomerGroupId", dbType: DbType.Guid, direction: ParameterDirection.Output);


            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckCustomerGroupNameExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                bool isExists = dynamicParameters.Get<bool>("@IsExists");
                var customerGroupId = dynamicParameters.Get<Guid>("@CustomerGroupId");

                return customerGroupId;
            }
        }

        public bool CheckCustomerPhoneNumberExists(string phoneNumber)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@PhoneNumber", phoneNumber);
            dynamicParameters.Add("@IsExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckCustomerPhoneNumberExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                bool isExists = dynamicParameters.Get<bool>("@IsExists");
                return isExists;
            }
        }

        public List<string> GetAllCustomerCode()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                return (List<string>)dbConnection.Query<string>("Proc_GetAllCustomerCode", commandType: CommandType.StoredProcedure);
            }
        }

        public List<string> GetAllPhoneNumber()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                return (List<string>)dbConnection.Query<string>("Proc_GetAllPhoneNumber", commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo phân trang
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">kích cỡ</param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Object GetByFilterPaging(string customerFilter, int pageSize, int pageIndex)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@CustomerFilter", customerFilter);
            parameters.Add("@Size", pageSize);
            parameters.Add("@Offset", (pageIndex - 1) * pageSize);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var customers = dbConnection.Query<Customer>("Proc_GetCustomerFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

                var data = new
                {
                    customers = (List<Customer>)customers,
                    totalPage = parameters.Get<Int32>("@TotalPage"),
                    totalRecord = parameters.Get<Int32>("@TotalRecord")
                };

                return data;
            }
        }
    }
}
