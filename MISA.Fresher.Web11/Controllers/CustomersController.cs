using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web11.Model;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet()]
        public int GetNumber(int number)
        {
            return number;
        }

        [HttpPost]
        public Customer PostCustomer(Customer customer)
        {
            return customer;
        }

        [HttpPut("{customerId}")]
        public object PutCustomer(Customer customer, string? customerId)
        {
            return new
            {
                customerId = customerId,
                data = customer
            };
        }

        [HttpDelete("{customerId}")]
        public string DeleteCustomer(string? customerId)
        {
            return customerId;
        }
    }
}
