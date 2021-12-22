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
        public Customer GetName2([FromBody] Customer customer)
        {
            return customer;
        }
    }
}
