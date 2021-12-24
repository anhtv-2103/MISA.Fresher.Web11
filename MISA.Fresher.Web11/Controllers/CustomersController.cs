using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web11.Model;
using Dapper;
using MySqlConnector;
using System.Data;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                //Khai báo thông tin db
                var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
                //Khởi tạo kết nối db
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //Thực hiện lấy dữ liệu trong db
                var sqlCommand = "SELECT * FROM Customer";
                var customers = sqlConnection.Query<object>(sqlCommand);

                //Trả dữ liệu cho client
                return Ok(customers);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }     
        }
        
        [HttpGet("{customerId}")]
        public IActionResult GetById(Guid customerId)
        {
            try
            {
                //Khai báo thông tin db
                var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
                //Khởi tạo kết nối db
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //Thực hiện lấy dữ liệu trong db
                var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId = @CustomerId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                var customers = sqlConnection.QueryFirstOrDefault<object>(sqlCommand, param:parameters);

                //Trả dữ liệu cho client
                return Ok(customers);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }     
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            try
            {
                //Khai báo thông tin lỗi
                List<string> errorMsgs = new List<string>();

                //Khai báo thông tin db
                var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
                //Khởi tạo kết nối db
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //Validate
                //- Mã khách hàng không được phép trùng hoặc để trống
                var customerCode = customer.CustomerCode;
                if (string.IsNullOrEmpty(customerCode))
                {
                    errorMsgs.Add("Mã khách hàng không được phép để trống!");
                }
                else
                {
                    var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
                    DynamicParameters paramCheck = new DynamicParameters();
                    paramCheck.Add("@CustomerCode", customerCode);
                    var customerDuplicate = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, param:paramCheck);
                    if(customerDuplicate != null)
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
                    return StatusCode(400, result);
                }

                //Sinh id mới
                customer.CustomerId = Guid.NewGuid();
                
                //Thực hiện thêm mới dữ liệu vào db
                var sqlCommand = "INSERT Customer (CustomerId, CustomerCode, FullName, PhoneNumber) " +
                                 "VALUES (@CustomerId, @CustomerCode, @FullName, @PhoneNumber)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.CustomerId);
                parameters.Add("@CustomerCode", customer.CustomerCode);
                parameters.Add("@FullName", customer.FullName);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);
                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                //Trả dữ liệu cho client
                if(res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }

        [HttpPut("{customerId}")]
        public IActionResult Put(Customer customer, Guid customerId)
        {
            try
            {
                //Khai báo thông tin lỗi
                List<string> errorMsgs = new List<string>();

                //Khai báo thông tin db
                var connectionString = "Server=47.241.69.179;Port=3306;Database=MISA.CukCuk_Demo_NVMANH_copy;User Id=dev;Password=manhmisa";
                //Khởi tạo kết nối db
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //Validate
                //- Mã khách hàng không được phép trùng hoặc để trống
                var customerCode = customer.CustomerCode;
                if (string.IsNullOrEmpty(customerCode))
                {
                    errorMsgs.Add("Mã khách hàng không được phép để trống!");
                }
                else
                {
                    var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode AND CustomerId != @CustomerId";
                    DynamicParameters paramCheck = new DynamicParameters();
                    paramCheck.Add("@CustomerCode", customerCode);
                    paramCheck.Add("@CustomerId", customerId);
                    var customerDuplicate = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: paramCheck);
                    if (customerDuplicate != null)
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
                    return StatusCode(400, result);
                }

                //Thực hiện update dữ liệu trong db
                var sqlCommand = "UPDATE Customer SET CustomerCode=@CustomerCode, FullName=@FullName, PhoneNumber=@PhoneNumber " +
                                 "WHERE CustomerId=@CustomerId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                parameters.Add("@CustomerCode", customer.CustomerCode);
                parameters.Add("@FullName", customer.FullName);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);
                var res = sqlConnection.Execute(sqlCommand, param: parameters);

                //Trả dữ liệu cho client
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer(Guid customerId)
        {
            try
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

                //Trả dữ liệu cho client
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }
    }
}
