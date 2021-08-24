using MISA.CukCuk.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Employee : BaseEnitiy
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên và đệm 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Ho ten day du
        /// </summary>
        [MISARequired]
        public string FullName { get; set; }

        /// <summary>
        /// Họ 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// id Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string GenderName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MISARequired]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MISARequired]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// CMTND hoặc CCCD
        /// </summary>
        [MISARequired]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày làm CMDTND hoắc CCCD
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi làm CMTND hoắc CCCD
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MartialStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? EducationalBackground { get; set; }

        /// <summary>
        /// Chất lượng
        /// </summary>
        public Guid? QualificationId { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Id vị tri
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Trạng thái công việc
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string PersonalTaxCode { get; set; }

        /// <summary>
        /// Tiền lương 
        /// </summary>
        public int? Salary { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string PositionCode { get; set; }

        #endregion
    }
}
