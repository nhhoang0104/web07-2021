using MISA.CukCuk.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Customer
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã Khách hàng
        /// </summary>
        [MISARequired]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Tên và đệm
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Họ
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        [MISARequired]
        public string FullName { get; set; }

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
        /// Id của nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DebitAmount { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Trạng thái công việc trong công ty: đang làm hoặc dừng
        /// </summary>
        public bool IsStopFollow { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        public string CustomerGroupName { get; set; }

        /// <summary>
        /// Trạng thái khi import
        /// </summary>
        public string StatusImport { get; set; } = string.Empty;

        /// <summary>
        /// true - thông tin khách hàng hợp lệ, false - không hợp lệ
        /// </summary>
        public bool IsValid { get; set; } = true;

        #endregion
    }
}
