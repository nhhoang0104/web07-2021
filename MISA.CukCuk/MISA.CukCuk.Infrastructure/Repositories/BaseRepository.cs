using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region Field
        protected string _connectionString = "Host=47.241.69.179;Database=MISA.CukCuk_NHHoang;User Id=dev;Password=12345678";
        private readonly string _modelName;
        #endregion

        #region Method
        public BaseRepository(IConfiguration configuration)
        {
            this._modelName = typeof(MISAEntity).Name;
            this._connectionString = configuration.GetConnectionString("sqlConnection");
        }

        public int Add(MISAEntity entity)
        {
            var parameters = new DynamicParameters();

            //Đọc từng properties:
            var props = entity.GetType().GetProperties();

            //Duyệt từng properties:
            foreach (var prop in props)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy giá trị của prop trong đối tượng:
                var propValue = prop.GetValue(entity);

                //Lấy kiểu dữ liệu của prop:
                var propType = prop.GetType();

                //Thêm param tương ứng với mỗi prop của đối tượng:
                parameters.Add($"@{propName}", propValue);
            }

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowEffect = dbConnection.Execute($"Proc_Insert{this._modelName}", param: parameters, commandType: CommandType.StoredProcedure);

                return rowEffect;
            }

        }

        public int Delete(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add($"@{this._modelName}Id", id);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowEffect = dbConnection.Execute($"Proc_Delete{this._modelName}ById", param: param, commandType: CommandType.StoredProcedure);

                return rowEffect;
            }
        }

        public List<MISAEntity> GetAll()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var entities = dbConnection.Query<MISAEntity>($"Proc_Get{this._modelName}s", commandType: CommandType.StoredProcedure);

                return (List<MISAEntity>)entities;
            }

        }

        public MISAEntity GetById(Guid id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add($"@{this._modelName}Id", id);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{this._modelName}ById", param: param, commandType: CommandType.StoredProcedure);

                return entity;
            }

        }

        public int Update(Guid id, MISAEntity entity)
        {
            var parameters = new DynamicParameters();

            //Đọc từng properties:
            var props = entity.GetType().GetProperties();

            //Duyệt từng properties:
            foreach (var prop in props)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy giá trị của prop trong đối tượng:
                var propValue = prop.GetValue(entity);

                //Lấy kiểu dữ liệu của prop:
                var propType = prop.GetType();

                //Thêm param tương ứng với mỗi prop của đối tượng
                if (propName == $"{this._modelName}Id")
                {
                    parameters.Add($"@{propName}", id);
                }
                else
                {
                    parameters.Add($"@{propName}", propValue);
                }

            }

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowEffect = dbConnection.Execute($"Proc_Update{this._modelName}", param: parameters, commandType: CommandType.StoredProcedure);

                return rowEffect;
            }


        }

        public Int32 DeleteEntities(List<Guid> entitiesId)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Open();
                var transaction = dbConnection.BeginTransaction();
                var rowEffect = 0;

                foreach (var id in entitiesId)
                {
                    DynamicParameters param = new DynamicParameters();

                    param.Add($"@{this._modelName}Id", id);
                    rowEffect += dbConnection.Execute($"Proc_Delete{this._modelName}ById", param: param, transaction: transaction, commandType: CommandType.StoredProcedure);
                }

                transaction.Commit();

                return rowEffect;
            }
        }

        #endregion

    }
}
