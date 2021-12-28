using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace MISA.WEB11.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public int Delete(Guid customerId)
        {
            //Khai báo thông tin db
            var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
            //Khởi tạo kết nối db
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Thực hiện xoá dữ liệu trong db
            var sqlCommand = "DELETE FROM Customer WHERE CustomerId=@CustomerId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            var res = sqlConnection.Execute(sqlCommand, param: parameters);
            return res;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            //Khai báo thông tin db
            var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
            //Khởi tạo kết nối db
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Thực hiện lấy dữ liệu trong db
            var sqlCommand = "SELECT * FROM Customer";
            var customers = sqlConnection.Query<Customer>(sqlCommand);

            //Trả dữ liệu
            return customers;
        }

        public Customer GetCustomerById(Guid customerId)
        {
            //Khai báo thông tin db
            var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
            //Khởi tạo kết nối db
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Thực hiện lấy dữ liệu trong db
            var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId = @CustomerId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            var customer = sqlConnection.QueryFirstOrDefault<Customer>(sqlCommand, param: parameters);

            //Trả dữ liệu
            return customer;
        }

        public int Insert(Customer customer)
        {
            //Khai báo thông tin db
            var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
            //Khởi tạo kết nối db
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Lấy dữ liệu trong db
            var sqlCommand = "INSERT Customer (CustomerId, CustomerCode, FullName, PhoneNumber) " +
                                 "VALUES (@CustomerId, @CustomerCode, @FullName, @PhoneNumber)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customer.CustomerId);
            parameters.Add("@CustomerCode", customer.CustomerCode);
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@PhoneNumber", customer.PhoneNumber);
            var res = sqlConnection.Execute(sqlCommand, param: parameters);
            return res;
        }

        public int Update(Customer customer, Guid customerId)
        {
            //Khai báo thông tin db
            var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
            //Khởi tạo kết nối db
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Lấy dữ liệu trong db
            var sqlCommand = "UPDATE Customer SET CustomerCode=@CustomerCode, FullName=@FullName, PhoneNumber=@PhoneNumber " +
                             "WHERE CustomerId=@CustomerId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            parameters.Add("@CustomerCode", customer.CustomerCode);
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@PhoneNumber", customer.PhoneNumber);
            var res = sqlConnection.Execute(sqlCommand, param: parameters);
            return res;
        }

        public bool CheckCustomerCodeDuplicate(string customerCode, Guid customerId)
        {
            //Khai báo thông tin db
            var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
            //Khởi tạo kết nối db
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Kiểm tra customerCode đã tồn tại hay chưa
            var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode AND CustomerId != @CustomerId";
            DynamicParameters paramCheck = new DynamicParameters();
            paramCheck.Add("@CustomerCode", customerCode);
            paramCheck.Add("@CustomerId", customerId);
            var customerDuplicate = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: paramCheck);
            return customerDuplicate != null;
        }
    }
}
