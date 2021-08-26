using MISA.CukCuk.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Customer
    {
        #region Property

        [MISAExport(true)]
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

        [Description("Mã khách hàng")]
        [MISAExport(true)]
        /// <summary>
        /// Mã Khách hàng
        /// </summary>
        [MISARequired]
        public string CustomerCode { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// Tên và đệm
        /// </summary>
        public string FirstName { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// Họ
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        [Description("Tên đầy đủ")]
        [MISARequired]
        [MISAExport(true)]
        public string FullName { get; set; }

        [Description("Địa chỉ")]
        [MISAExport(true)]
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// id Giới tính
        /// </summary>
        public int? Gender { get; set; }

        [Description("Giới tính")]
        [MISAExport(true)]
        /// <summary>
        /// Giới tính
        /// </summary>
        public string GenderName { get; set; }

        [Description("Ngày sinh")]
        [MISAExport(true)]
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        [Description("Email")]
        [MISAExport(true)]
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        [Description("Số điện thoại")]
        [MISAExport(true)]
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// Id của nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// 
        /// </summary>
        public string DebitAmount { get; set; }

        [Description("Mã thẻ thành viên")]
        [MISAExport(true)]
        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string CompanyTaxCode { get; set; }

        [Description("Quan tâm")]
        [MISAExport(true)]
        /// <summary>
        /// Trạng thái: quan tâm hay ko
        /// </summary>
        public bool IsStopFollow { get; set; }

        [Description("Nhóm khách hàng")]
        [MISAExport(true)]
        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        public string CustomerGroupName { get; set; }

        [MISAExport(false)]
        /// <summary>
        /// Trạng thái khi import
        /// </summary>
        public string StatusImport { get; set; } = string.Empty;

        [MISAExport(false)]
        /// <summary>
        /// true - thông tin khách hàng hợp lệ, false - không hợp lệ
        /// </summary>
        public bool IsValid { get; set; } = true;

        #endregion
    }
}
