using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class CustomerGroup : BaseEnitiy
    {
        /// <summary>
        /// id nhóm kh
        /// </summary>
        public Guid CustomerGroupId { get; set; }

        /// <summary>
        /// nhóm khách hàng
        /// </summary>
        public string CustomerGroupName { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string Description { get; set; }
    }
}
