using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroppboxApi.Models;

namespace DroppboxApi.Controllers
{
    

    [Route("DroppboxAPI")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DroppboxContext _context;

        public UserController(DroppboxContext context)
        {
            _context = context;
        }
         [HttpGet("User")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            System.Console.WriteLine("Gettsta");
            Console.WriteLine(_context.users);
            return await _context.users.ToListAsync();
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var User = await _context.users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

    
        [HttpPost("User")]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            System.Console.WriteLine(User);
            _context.users.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = User.id }, User);
        }

        [HttpPut("User/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> PutUser(long id, User User)
        {
            if (id != User.id)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.users.ToListAsync();
        }

        [HttpDelete("User/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteUser(long id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            Console.WriteLine("delet API");
            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return await _context.users.ToListAsync();
        }

    }
}
