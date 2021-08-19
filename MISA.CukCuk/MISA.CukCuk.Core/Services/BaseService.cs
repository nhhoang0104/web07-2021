using MISA.CukCuk.Core.Attributes;
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

        public virtual ServiceResult Add(MISAEntity entity)
        {
            if (!this.ValidateData(entity)) return this._serviceResult;

            if (!this.ValidateCustom(entity)) return this._serviceResult;

            this._serviceResult.Data = this._baseRepository.Add(entity);

            return this._serviceResult;
        }

        public ServiceResult GetAll()
        {
            this._serviceResult.Data = this._baseRepository.GetAll();

            return this._serviceResult;
        }

        public ServiceResult GetById(string id)
        {
            if (!this.ValidateId(id)) return this._serviceResult;

            this._serviceResult.Data = this._baseRepository.GetById(Guid.Parse(id));

            return this._serviceResult;
        }

        public virtual ServiceResult Update(string id, MISAEntity entity)
        {
            if (!this.ValidateId(id)) return this._serviceResult;

            if (!this.ValidateData(entity)) return this._serviceResult;

            if (!this.ValidateCustom(entity)) return this._serviceResult;

            this._serviceResult.Data = this._baseRepository.Update(Guid.Parse(id), entity);

            return this._serviceResult;
        }

        public ServiceResult DeleteEntites(List<string> entitiesId)
        {
            var tmp = new List<Guid>();
            foreach (var id in entitiesId)
            {
                if (!this.ValidateId(id)) return this._serviceResult;

                tmp.Add(Guid.Parse(id));
            }

            this._serviceResult.Data = this._baseRepository.DeleteEntities(tmp);

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

                this._serviceResult.Messager = Resources.ErrorMessage.IDInvalid_ErrorMsg;
            }

            return isGuid;
        }

        /// <summary>
        /// validate chung các trường trong thông tin bắt buộc
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected bool ValidateData(MISAEntity entity)
        {
            var isValid = true;

            var properties = typeof(MISAEntity).GetProperties();

            foreach (var prop in properties)
            {
                var propVal = prop.GetValue(entity);
                var propName = prop.Name;
                var propMISARequired = prop.GetCustomAttributes(typeof(MISARequired), true);

                if (propMISARequired.Length > 0)
                {
                    if (prop.PropertyType == typeof(string) && (propName == null || propVal.ToString() == String.Empty))
                    {
                        isValid = false;
                        var nameErrorMsg = $"{propName}_ErrorMsg";
                        _serviceResult.IsValid = false;
                        _serviceResult.Messager = Resources.ErrorMessage.PropRequired_ErrorMsg;
                    }
                }

            }

            return isValid;
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
