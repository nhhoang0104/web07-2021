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
    class PositionRepository : IPositionRepository
    {
        private IDbConnection _dbConnection;
        private string config = "Host=47.241.69.179;Database=MISA.CukCuk_NHHoang;User Id=dev;Password=12345678";

        public PositionRepository()
        {
            this._dbConnection = new MySqlConnection(config);
        }

        public int Add(Position position)
        {

            throw new NotImplementedException();
        }

        public int Delete(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@PositionId", id);

            var rowEffect = this._dbConnection.Execute("Proc_DeletePositionById", param: param, commandType: CommandType.StoredProcedure);

            return rowEffect;
        }

        public List<Position> Get()
        {
            var deparments = this._dbConnection.Query<Position>("Proc_GetPositions", commandType: CommandType.StoredProcedure);

            return (List<Position>)deparments;
        }

        public Position GetById(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@PositionId", id);

            var position = this._dbConnection.QueryFirstOrDefault<Position>("Proc_GetPositionById", param: param, commandType: CommandType.StoredProcedure);

            return position;
        }

        public int Update(Guid id, Position position)
        {
            throw new NotImplementedException();
        }
    }
}
