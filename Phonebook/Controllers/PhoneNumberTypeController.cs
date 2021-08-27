using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [Route("v1/phonetypes")]
    public class PhoneNumberTypeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<PhoneNumberType>>> Get(
            [FromServices] DataContext context
        )
        {
            var phoneNumberType = await context.PhoneNumberTypes.AsNoTracking().ToListAsync();
            return Ok(phoneNumberType);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PhoneNumberType>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            var phoneNumberType = await context.PhoneNumberTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(phoneNumberType);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<PhoneNumberType>>> Post(
            [FromBody] PhoneNumberType model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.PhoneNumberTypes.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível cadastrar o tipo de telefone" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<PhoneNumberType>>> Put(
            int id,
            [FromBody] PhoneNumberType model,
            [FromServices] DataContext context)
        {
            if (id != model.Id)
                return NotFound(new { message = "Tipo de telefone não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<PhoneNumberType>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o tipo de telefone" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<PhoneNumberType>>> Delete(
            int id,
            [FromServices] DataContext context)
        {
            var phoneNumberType = await context.PhoneNumberTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (phoneNumberType == null)
                return NotFound(new { message = "Tipo de telefone não encontrado" });

            try
            {
                context.PhoneNumberTypes.Remove(phoneNumberType);
                await context.SaveChangesAsync();
                return Ok(new { message = "Tipo de telefone removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o tipo de telefone" });

            }
        }
    }
}