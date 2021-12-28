using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Entities
{
    /// <summary>
    /// Thông tin khách hàng
    /// CreatedBy: TVANH (27/12/2021)
    /// </summary>
    public class Customer
    {
        #region Property
        public Guid CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        #endregion
    }
}
