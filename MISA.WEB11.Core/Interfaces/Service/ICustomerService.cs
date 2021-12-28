using MISA.WEB11.Core.Entities;

namespace MISA.WEB11.Core.Interfaces.Service
{
    public interface ICustomerService
    {
        /// <summary>
        /// Validate dữ liệu khi thêm mới khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreatedBy: TVANH (28/12/2021)
        int InsertService(Customer customer);

        /// <summary>
        /// Validate dữ liệu khi sửa thông tin khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: TVANH (28/12/2021)
        int UpdateService(Customer customer, Guid customerId);
    }
}
