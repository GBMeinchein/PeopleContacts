using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleContacts.Domain;
using PeopleContacts.Domain.Entitys;
using PeopleContacts.Repository;

namespace PeopleContacts.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PeopleContactsContext _context;

        public PersonController(PeopleContactsContext context)
        {
            _context = context;

            if (_context.People.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.People.Add(new Person { Nome = "Gabriel" });
                _context.SaveChanges();
            }
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(long id)
        {
           return await _context.People.FindAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();

            var retorno = CreatedAtAction(nameof(Get), new { id = person.Id }, person);
            return retorno;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoItem = await _context.People.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.People.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
