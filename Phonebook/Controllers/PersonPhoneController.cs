using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [Route("v1/personphones")]
    public class PersonPhoneController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<PersonPhone>>> Get(
            [FromServices] DataContext context
        )
        {
            var personPhone = await context
            .PersonPhones
            .Include(x => x.PhoneNumberType)
            .AsNoTracking()
            .ToListAsync();
            return Ok(personPhone);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PersonPhone>> GetById(
        int id,
        [FromServices] DataContext context)
        {
            var personPhone = await context
            .PersonPhones
            .Include(x => x.PhoneNumberType)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(personPhone);
        }

        [HttpGet]
        [Route("phonetypes/{id:int}")]
        public async Task<ActionResult<PersonPhone>> GetByPhoneNumberType(
        int id,
        [FromServices] DataContext context)
        {
            var personPhone = await context
            .PersonPhones
            .Include(x => x.PhoneNumberType)
            .AsNoTracking()
            .Where(x => x.PhoneNumberTypeID == id)
            .ToListAsync();
            return Ok(personPhone);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<PersonPhone>>> Post(
        [FromBody] PersonPhone model,
        [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.PersonPhones.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível cadastrar o telefone" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<PersonPhone>>> Put(
            int id,
            [FromBody] PersonPhone model,
            [FromServices] DataContext context)
        {
            if (id != model.Id)
                return NotFound(new { message = "Telefone não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<PersonPhone>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o telefone" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<PersonPhone>>> Delete(
            int id,
            [FromServices] DataContext context)
        {
            var personPhone = await context.PersonPhones.FirstOrDefaultAsync(x => x.Id == id);
            if (personPhone == null)
                return NotFound(new { message = "Telefone não encontrado" });

            try
            {
                context.PersonPhones.Remove(personPhone);
                await context.SaveChangesAsync();
                return Ok(new { message = "Telefone removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o telefone" });

            }
        }
    }
}
