using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var phoneNumberType = new PhoneNumberType { Id = 1, Name = "Personal" };
            var personPhone = new PersonPhone { Id = 1, PhoneNumberType = phoneNumberType, PhoneNumber = "11.99999.9999", PhoneNumberTypeID = 1 };
            context.PhoneNumberTypes.Add(phoneNumberType);
            context.PersonPhones.Add(personPhone);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}