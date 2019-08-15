using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleContacts.Domain;
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
                _context.People.Add(new Person { Nome = "Gabriel Bruno Meinchein" });
                _context.SaveChanges();
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(long id)
        {
            return await _context.People.Include(p => p.Contacts).Where(w => w.Id == id).FirstAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();

            var retorno = CreatedAtAction(nameof(Get), new { id = person.Id }, person);
            return retorno;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            Person updPerson = new Person();
            updPerson.Id = id;
            updPerson.Nome = person.Nome;
            _context.People.Update(updPerson);

            var personDataBase = _context.People.Include(p => p.Contacts).Where(w => w.Id == id).First();
            foreach (var item in personDataBase.Contacts)
            {
                _context.Contacts.Remove(item);
            }

            foreach (var contact in person.Contacts)
            {
                contact.PersonId = person.Id;
                _context.Contacts.Add(contact);
            }

            await _context.SaveChangesAsync();

            var retorno = CreatedAtAction(nameof(Get), new { id = person.Id }, person);
            return retorno;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoItem = _context.People.Include(p => p.Contacts).Where(w => w.Id == id).First();

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Contacts.RemoveRange(todoItem.Contacts);
            _context.People.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
