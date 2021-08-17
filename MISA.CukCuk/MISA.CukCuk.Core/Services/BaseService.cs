using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            this._baseRepository = baseRepository;
            this._serviceResult = new ServiceResult();
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">thông tin</param>
        /// <returns></returns>
        public ServiceResult Add(MISAEntity entity)
        {
            if (!this.ValidateData(entity)) return this._serviceResult;

            if (!this.ValidateCustom(entity)) return this._serviceResult;

            this._serviceResult.Data = this._baseRepository.Add(entity);

            return this._serviceResult;
        }

        /// <summary>
        /// Xóa theo id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ServiceResult Delete(string id)
        {
            if (!this.ValidateId(id)) return this._serviceResult;

            this._serviceResult.Data = this._baseRepository.Delete(Guid.Parse(id));

            return this._serviceResult;
        }

        /// <summary>
        /// Lấy tât cả
        /// </summary>
        /// <returns>danh sách</returns>
        public ServiceResult GetAll()
        {
            this._serviceResult.Data = this._baseRepository.GetAll();

            return this._serviceResult;
        }

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>thông tin</returns>
        public ServiceResult GetById(string id)
        {
            if (!this.ValidateId(id)) return this._serviceResult;

            this._serviceResult.Data = this._baseRepository.GetById(Guid.Parse(id));

            return this._serviceResult;
        }

        /// <summary>
        /// cập nhật thông tin
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">thông tin</param>
        /// <returns></returns>
        public ServiceResult Update(string id, MISAEntity entity)
        {
            if (!this.ValidateId(id)) return this._serviceResult;

            if (!this.ValidateData(entity)) return this._serviceResult;

            if (!this.ValidateCustom(entity)) return this._serviceResult;

            this._serviceResult.Data = this._baseRepository.Update(Guid.Parse(id), entity);

            return this._serviceResult;
        }

        /// <summary>
        /// Validate id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>
        /// true - id hợp lệ
        /// false - id không hợp lệ
        /// </returns>
        private bool ValidateId(string id)
        {
            Guid tmp;

            bool isGuid = Guid.TryParse(id.ToString(), out tmp);

            if (!isGuid)
            {
                this._serviceResult.IsValid = false;

                this._serviceResult.Messager = "Id không hợp lệ";
            }

            return isGuid;
        }

        /// <summary>
        /// validate chung các trường trong thông tin
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool ValidateData(MISAEntity entity)
        {
            return true;
        }

        /// <summary>
        /// Validate các trường thông tin theo lớp cụ thể
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool ValidateCustom(MISAEntity entity)
        {
            return true;
        }
    }
}
