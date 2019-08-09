using System;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
