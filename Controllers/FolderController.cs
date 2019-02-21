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
    public class FolderController : ControllerBase
    {
        private readonly DroppboxContext _context;

        public FolderController(DroppboxContext context)
        {
            
        }
        
        [HttpGet("Folder")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolder()
        {
            return await _context.folders.ToListAsync();
        }

        [HttpGet("Folder/{id}")]
        public async Task<ActionResult<Folder>> GetFolder(long id)
        {
            var folder = await _context.folders.FindAsync(id);

            if (folder == null)
            {
                return NotFound();
            }

            return folder;
        }

        [HttpPost("Folder")]
        public async Task<ActionResult<Folder>> PostFolder(Folder folder)
        {
            _context.folders.Add(folder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFile", new { id = folder.id }, folder);
        }

        [HttpPut("Folder/{id}")]
        public async Task<ActionResult<IEnumerable<Folder>>> PutFolder(long id, Folder folder)
        {
            if (id != folder.id)
            {
                return BadRequest();
            }

            _context.Entry(folder).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.folders.ToListAsync();
        }

        [HttpDelete("Folder/{id}")]
        public async Task<ActionResult<IEnumerable<Folder>>> DeleteFolder(long id)
        {

            var folder = await _context.folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound();}
            
            Console.WriteLine("delet API");
            _context.folders.Remove(folder);
            await _context.SaveChangesAsync();

            return await _context.folders.ToListAsync();
        }
    }
}
