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

namespace MISA.CukCuk.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private IDbConnection _dbConnection;
        private string config = "Host=47.241.69.179;Database=MISA.CukCuk_NHHoang;User Id=dev;Password=12345678";

        public DepartmentRepository()
        {
            this._dbConnection = new MySqlConnection(config);
        }

        public int Add(Department department)
        {

            throw new NotImplementedException();
        }

        public int Delete(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@DepartmentId", id);

            var rowEffect = this._dbConnection.Execute("Proc_DeleteDepartmentById", param: param, commandType: CommandType.StoredProcedure);

            return rowEffect;
        }

        public List<Department> Get()
        {
            var deparments = this._dbConnection.Query<Department>("Proc_GetDepartments", commandType: CommandType.StoredProcedure);

            return (List<Department>)deparments;
        }

        public Department GetById(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@DepartmentId", id);

            var deparment = this._dbConnection.QueryFirstOrDefault<Department>("Proc_GetDepartmentById", param: param, commandType: CommandType.StoredProcedure);

            return deparment;
        }

        public int Update(Guid id, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
