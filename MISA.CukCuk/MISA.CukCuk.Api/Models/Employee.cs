using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Models
{
    public class Employee : Entity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên và đệm 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Ho ten day du
        /// </summary>
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
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// CMTND hoặc CCCD
        /// </summary>
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
        public double Salary { get; set; }

        #endregion
    }
}
