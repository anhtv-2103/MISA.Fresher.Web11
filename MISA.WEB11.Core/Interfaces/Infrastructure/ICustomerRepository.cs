using MISA.WEB11.Core.Entities;

namespace MISA.WEB11.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface sử dụng cho Khách hàng
    /// CreatedBy: TVANH (27/12/2021)
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: TVANH (27/12/2021)
        IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Lấy thông tin khách hàng theo id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: TVANH (28/12/2021)
        Customer GetCustomerById(Guid customerId);

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>'
        /// CreatedBy: TVANH (28/12/2021)
        int Insert(Customer customer);

        /// <summary>
        /// Sửa thông tin khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: TVANH (28/12/2021)
        int Update(Customer customer, Guid customerId);

        /// <summary>
        /// Xoá khách hàng
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: TVANH (28/12/2021)
        int Delete(Guid customerId);

        /// <summary>
        /// Kiểm tra mã khách hàng đã tồn tại hay chưa
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: TVANH (28/12/2021)
        bool CheckCustomerCodeDuplicate(string customerCode, Guid customerId);
    }
}
