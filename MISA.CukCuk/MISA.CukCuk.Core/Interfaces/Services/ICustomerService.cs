using Microsoft.AspNetCore.Http;
using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Xử lý, lọc dữ liệu trước khi import
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken);

    }
}
