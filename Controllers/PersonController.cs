using ConnectionLibrary.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConnectionLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConnectionLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonController(DataContext context)
        {
            _context = context;
        }

        [HttpGet] // R in CRUD
        public async Task<ActionResult<List<Person>>> GetAllPeople() // returns a list of all people in DB
        {
            var people = await _context.People.ToListAsync();
            if (people.Count == 0)
            {
                return BadRequest("No people found.");
            }
            return Ok(people);
        }

        [HttpGet("{id}")] // R in CRUD
        public async Task<ActionResult<Person>> GetPerson(int id) // returns one person from the DB if found
        {
            var person = await _context.People.FindAsync(id); // finds the person based on id
            if (person == null)
            {
                return BadRequest("Person not found.");
            }
            return Ok(person);
        }

        [HttpPost] // C in CRUD
        public async Task<ActionResult<List<Person>>> AddPerson(Person person) // adds a person to DB given all of its characteristics
        {
            if (string.IsNullOrEmpty(person.LinkedIn))
            {
                person.LinkedIn = null;
            }
            if (string.IsNullOrEmpty(person.Email))
            {
                person.Email = null;
            }
            if (string.IsNullOrEmpty(person.PhoneNumber))
            {
                person.PhoneNumber = null;
            }
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return Ok(await _context.People.ToListAsync());
        }

        [HttpPut] // U in CRUD
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person updatedPerson)
        {
            var dbPerson = await _context.People.FindAsync(updatedPerson.Id);
            if (dbPerson == null)
            {
                return BadRequest("Person not found.");
            }
            dbPerson.FirstName = updatedPerson.FirstName;
            dbPerson.LastName = updatedPerson.LastName;
            dbPerson.LinkedIn = updatedPerson.LinkedIn;
            dbPerson.Email = updatedPerson.Email;
            dbPerson.PhoneNumber = updatedPerson.PhoneNumber;
            if (string.IsNullOrEmpty(dbPerson.LinkedIn))
            {
                dbPerson.LinkedIn = null;
            }
            if (string.IsNullOrEmpty(dbPerson.Email))
            {
                dbPerson.Email = null;
            }
            if (string.IsNullOrEmpty(dbPerson.PhoneNumber))
            {
                dbPerson.PhoneNumber = null;
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.People.ToListAsync());
        }

        [HttpDelete("{id}")] // D in CRUD
        public async Task<ActionResult<List<Person>>> DeletePerson(int id)
        {
            var dbHero = await _context.People.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Person not found.");
            }
            _context.People.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.People.ToListAsync());
        }
    }
}
