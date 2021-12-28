using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Exceptions;
using MISA.WEB11.Core.Interfaces.Infrastructure;
using MISA.WEB11.Core.Interfaces.Service;

namespace MISA.WEB11.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;

        public CustomersController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _customerRepository.GetCustomers();
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
                var customers = _customerRepository.GetCustomerById(customerId);
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
        public IActionResult Insert(Customer customer)
        {
            try
            {
                var res = _customerService.InsertService(customer);
                if(res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch(MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
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
        public IActionResult Update(Customer customer, Guid customerId)
        {
            try
            {
                var res = _customerService.UpdateService(customer, customerId);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
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
        public IActionResult Delete(Guid customerId)
        {
            try
            {
                var res = _customerRepository.Delete(customerId);
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
