using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repositories
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns></returns>
        List<MISAEntity> GetAll();

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MISAEntity GetById(Guid id);

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Int32 Add(MISAEntity entity);

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Int32 Update(Guid id, MISAEntity entity);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Int32 Delete(Guid id);

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="entitiesId"></param>
        /// <returns></returns>
        Int32 DeleteEntities(List<Guid> entitiesId);
    }
}
