using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Xử lý nghiệp vụ trước lấy tất cả danh sách 
        /// </summary>
        /// <returns>
        /// trả về 1 service
        /// {
        ///     isValid = fasle => bade resquest
        /// }
        /// </returns>
        ServiceResult GetAll();

        /// <summary>
        /// Xử lý nghiệp vụ( validateId) trước khi lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>thông tin nhân viên theo id</returns>
        ServiceResult GetById(string id);

        /// <summary>
        /// Xử lý nghiệp vụ validate các dữ liệu đầu vào trước khi thêm mới
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        ServiceResult Add(MISAEntity entity);

        /// <summary>
        /// Xử lý nghiệp vụ validate các dữ liệu đầu,id vào trước khi cập nhật
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        ServiceResult Update(string id, MISAEntity entity);

        /// <summary>
        ///  Xử lý nghiệp vụ trước xóa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult DeleteEntites(List<string> entitiesId);
    }
}
