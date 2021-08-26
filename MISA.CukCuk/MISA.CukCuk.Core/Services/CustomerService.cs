using Microsoft.AspNetCore.Http;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Repositories;
using MISA.CukCuk.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;
        ICustomerGroupRepository _customerGroupRepository;
        public CustomerService(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository, ICustomerGroupRepository customerGroupRepository) : base(baseRepository)
        {
            this._customerRepository = customerRepository;
            this._customerGroupRepository = customerGroupRepository;
        }

        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            var customers = new List<Customer>();
            var phoneNumberList = new List<string>();
            var customerCodeList = new List<string>();

            // đọc dữ liệu trong file .xlsx .xls
            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, cancellationToken);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        DateTime? test = this.TransformDateTime(worksheet.Cells[row, 6].Value);

                        var phoneNumber = this.TransformString(worksheet.Cells[row, 5].Value);
                        var customerCode = this.TransformString(worksheet.Cells[row, 1].Value);

                        phoneNumberList.Add(phoneNumber);
                        customerCodeList.Add(customerCode);

                        customers.Add(new Customer()
                        {
                            CustomerCode = customerCode,
                            FullName = this.TransformString(worksheet.Cells[row, 2].Value),
                            MemberCardCode = this.TransformString(worksheet.Cells[row, 3].Value),
                            CustomerGroupName = this.TransformString(worksheet.Cells[row, 4].Value),
                            PhoneNumber = phoneNumber,
                            DateOfBirth = this.TransformDateTime(worksheet.Cells[row, 6].Value),
                            CompanyName = this.TransformString(worksheet.Cells[row, 7].Value),
                            CompanyTaxCode = this.TransformString(worksheet.Cells[row, 8].Value),
                            Email = this.TransformString(worksheet.Cells[row, 9].Value),
                            Address = this.TransformString(worksheet.Cells[row, 10].Value),
                        });
                    }


                }

            }

            customers = this.ValidatePreImport(customers);

            this._serviceResult.Data = customers;

            return this._serviceResult;
        }

        /// <summary>
        /// Chuyển đổi object to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string TransformString(object value)
        {
            if (value == null) return String.Empty;
            return value.ToString().Trim();

        }

        /// <summary>
        /// Chuyển đổi object to datetime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        DateTime? TransformDateTime(object value)
        {
            if (value != null)
            {
                string tmp = value.ToString().Trim();
                // dd/MM/yyyy
                if (Regex.IsMatch(tmp, @"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$"))
                {
                    return DateTime.ParseExact(tmp, "dd/MM/yyyy", null);
                }

                if (Regex.IsMatch(tmp, @"^(((0)[0-9])|((1)[0-2]))(\/)\d{4}$"))
                {
                    return DateTime.ParseExact("01/" + tmp, "dd/MM/yyyy", null);
                }

                if (Regex.IsMatch(tmp, @"^\d{4}$"))
                {
                    return DateTime.ParseExact("01/01/" + tmp, "dd/MM/yyyy", null);
                }
            }

            return null;
        }

        /// <summary>
        /// validate danh sach thonh tin khach hang trước khi import
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="phoneNumberList"></param>
        /// <param name="customerCodeList"></param>
        /// <returns></returns>
        List<Customer> ValidatePreImport(List<Customer> customers)
        {
            List<string> customerCodeList = this._customerRepository.GetAllCustomerCode();
            List<string> phoneNumberList = this._customerRepository.GetAllPhoneNumber();
            List<CustomerGroup> customerGroupList = _customerGroupRepository.GetAll();
            List<string> phoneNumberImportList = new List<string>();
            List<string> customerCodeImportList = new List<string>();

            foreach (var customer in customers)
            {
                //Check FullName
                if (customer.FullName == string.Empty)
                {
                    customer.StatusImport += Resources.ErrorMessage.FullName_ErrorMsg + ", ";
                    customer.IsValid = false;
                }

                //Check MKH
                if (customer.CustomerCode != string.Empty)
                {
                    if (customerCodeList.IndexOf(customer.CustomerCode) != -1)
                    {
                        customer.StatusImport += Resources.ErrorMessage.CustometCodeExists_Msg + ", ";
                        customer.IsValid = false;
                    }

                    if (customerCodeImportList.IndexOf(customer.CustomerCode) != -1)
                    {
                        customer.StatusImport += Resources.ErrorMessage.CustomerCodeExistsInFile + ", ";
                        customer.IsValid = false;
                    }
                    else
                    {
                        customerCodeImportList.Add(customer.CustomerCode);
                    }
                }
                else
                {
                    customer.IsValid = false;
                    customer.StatusImport += Resources.ErrorMessage.CustomerCode_ErrorMsg + ", ";
                }

                //Check SDT
                if (customer.PhoneNumber != String.Empty)
                {
                    if (phoneNumberList.IndexOf(customer.PhoneNumber) != -1)
                    {
                        customer.IsValid = false;
                        customer.StatusImport += Resources.ErrorMessage.PhoneNumberExsist_ErrorMsg + ", ";
                    }
                    else if (phoneNumberImportList.IndexOf(customer.PhoneNumber) != -1)
                    {
                        customer.IsValid = false;
                        customer.StatusImport += Resources.ErrorMessage.PhoneNumberExistsInFile + ", ";
                    }
                    else
                    {
                        phoneNumberImportList.Add(customer.PhoneNumber);
                    }
                }

                var customerGroup = customerGroupList.Find(delegate (CustomerGroup item) { return item.CustomerGroupName == customer.CustomerGroupName; });

                //Chech ten nhom khach hang
                if (customerGroup == null)
                {
                    customer.IsValid = false;
                    customer.StatusImport += Resources.ErrorMessage.CustomerGroupNameExists_ErrroMsg + ", ";
                }
                else
                {
                    customer.CustomerGroupId = customerGroup.CustomerGroupId;
                }

                if (customer.StatusImport != string.Empty) customer.StatusImport = customer.StatusImport.Remove(customer.StatusImport.Length - 2);

                if (customer.IsValid)
                {
                    customer.StatusImport = "Hợp lệ";
                }
            }

            return customers;
        }
    }
}
