using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class ServiceResult
    {
        #region Property
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// thông tin kết quả
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Tin nhắn
        /// </summary>
        public string Messager { get; set; }

        #endregion
    }
}
