using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Exceptions;
using MISA.WEB11.Core.Interfaces.Infrastructure;
using MISA.WEB11.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Service
{
    public class CustomerService : ICustomerService
    {
        #region Fields
        List<string> errorMsgs = new List<string>();
        ICustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region Methods
        public int InsertService(Customer customer)
        {
            customer.CustomerId = Guid.NewGuid();
            //Validate
            //- Mã khách hàng không được phép trùng hoặc để trống
            var customerCode = customer.CustomerCode;
            var customerId = customer.CustomerId;
            if (string.IsNullOrEmpty(customerCode))
            {
                errorMsgs.Add("Mã khách hàng không được phép để trống!");
            }
            else
            {
                //Thực hiện kiểm tra trùng mã trong db
                var isDuplicate = _customerRepository.CheckCustomerCodeDuplicate(customerCode, customerId);
                if (isDuplicate)
                {
                    errorMsgs.Add("Mã khách hàng không được phép trùng!");
                }
            }
            //- Số điện thoại không được phép trống
            if (string.IsNullOrEmpty(customer.PhoneNumber))
            {
                errorMsgs.Add("Số điện thoại không được phép để trống");
            }
            //- Tên không được phép trống
            if (string.IsNullOrEmpty(customer.FullName))
            {
                errorMsgs.Add("Họ và tên không được phép để trống");
            }

            if (errorMsgs.Count > 0)
            {
                var result = new
                {
                    userMsg = "Dữ liệu không hợp lệ",
                    data = errorMsgs
                };
                throw new MISAValidateException(result);
            }
            else
            {
                return _customerRepository.Insert(customer);
            }
        }

        public int UpdateService(Customer customer, Guid customerId)
        {
            //Validate
            //- Mã khách hàng không được phép trùng hoặc để trống
            var customerCode = customer.CustomerCode;
            if (string.IsNullOrEmpty(customerCode))
            {
                errorMsgs.Add("Mã khách hàng không được phép để trống!");
            }
            else
            {
                //Thực hiện kiểm tra trùng mã trong db
                var isDuplicate = _customerRepository.CheckCustomerCodeDuplicate(customerCode, customerId);
                if (isDuplicate)
                {
                    errorMsgs.Add("Mã khách hàng không được phép trùng!");
                }
            }
            //- Số điện thoại không được phép trống
            if (string.IsNullOrEmpty(customer.PhoneNumber))
            {
                errorMsgs.Add("Số điện thoại không được phép để trống");
            }
            //- Tên không được phép trống
            if (string.IsNullOrEmpty(customer.FullName))
            {
                errorMsgs.Add("Họ và tên không được phép để trống");
            }

            if (errorMsgs.Count > 0)
            {
                var result = new
                {
                    userMsg = "Dữ liệu không hợp lệ",
                    data = errorMsgs
                };
                throw new MISAValidateException(result);
            }
            else
            {
                return _customerRepository.Update(customer, customerId);
            }
        }
        #endregion
    }
}
