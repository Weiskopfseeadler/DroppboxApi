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
    public class FileController : ControllerBase
    {
        private readonly DroppboxContext _context;

        public FileController(DroppboxContext context)
        {
            _context = context;
        }
        
        [HttpGet("File")]
        public async Task<ActionResult<IEnumerable<File>>> GetFile()
        {
            return await _context.files.ToListAsync();
        }

        [HttpGet("File/{id}")]
        public async Task<ActionResult<File>> GetFile(long id)
        {
            var File = await _context.files.FindAsync(id);

            if (File == null)
            {
                return NotFound();
            }

            return File;
        }

        [HttpPost("File")]
        public async Task<ActionResult<File>> PostFile(File file)
        {
            _context.files.Add(file);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFile", new { id = file.id }, file);
        }

        [HttpPut("File/{id}")]
        public async Task<ActionResult<IEnumerable<File>>> PutFile(long id, File file)
        {
            if (id != file.id)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.files.ToListAsync();
        }

        [HttpDelete("File/{id}")]
        public async Task<ActionResult<IEnumerable<File>>> DeleteFile(long id)
        {

            var File = await _context.files.FindAsync(id);
            if (File == null)
            {
                return NotFound();
            }
            
            Console.WriteLine("delet API");
            _context.files.Remove(File);
            await _context.SaveChangesAsync();

            return await _context.files.ToListAsync();
        }


    }
}
