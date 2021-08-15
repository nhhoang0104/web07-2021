using Dapper;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Respositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IDbConnection _dbConnection;
        private string config = "Host=47.241.69.179;Database=MISA.CukCuk_NHHoang;User Id=dev;Password=12345678";
        public EmployeeRepository()
        {
            this._dbConnection = new MySqlConnection(config);
        }

        public List<Employee> Get(string employeeFilter, Guid? departmentId, Guid? positionId, int pageSize, int pageIndex)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@EmployeeFilter", employeeFilter);
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@PositionId", positionId);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employees = this._dbConnection.Query<Employee>("Proc_GetEmployeeFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

            return (List<Employee>)employees;
        }

        public Employee GetById(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@EmployeeId", id);

            var empployee = this._dbConnection.QueryFirstOrDefault<Employee>("Proc_GetEmployeeById", param: param, commandType: CommandType.StoredProcedure);

            return empployee;
        }

        public int Add(Employee employee)
        {
            var parameters = new DynamicParameters();

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
                parameters.Add($"@{propName}", propValue);
            }

            var rowEffect = this._dbConnection.Execute("Proc_InsertEmployee", param: parameters, commandType: CommandType.StoredProcedure);

            return rowEffect;
        }

        public int Update(Guid id, Employee employee)
        {
            var parameters = new DynamicParameters();
            employee.EmployeeId = id;

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
                parameters.Add($"@{propName}", propValue);
            }

            var rowEffect = this._dbConnection.Execute("Proc_UpdateEmployee", param: parameters, commandType: CommandType.StoredProcedure);

            return rowEffect;
        }

        public int Delete(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@EmployeeId", id);

            var rowEffect = this._dbConnection.Execute("Proc_DeleteEmployeeById", param: param, commandType: CommandType.StoredProcedure);

            return rowEffect;
        }
    }
}
