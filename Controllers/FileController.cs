using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroppboxApi.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

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
        public async Task<ActionResult<IEnumerable<Models.File>>> GetFile()
        {
            return await _context.files.ToListAsync();
        }

        [HttpGet("File/{id}")]
        public async Task<ActionResult<Models.File>> GetFile(long id)
        {
            var File = await _context.files.FindAsync(id);

            if (File == null)
            {
                return NotFound();
            }

            return File;
        }
/*
        [HttpPost("File")]
        public async Task<ActionResult<File>> PostFile(File file)
        {
            _context.files.Add(file);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFile", new { id = file.id }, file);
        }
*/
        [HttpPost("file"),DisableRequestSizeLimit]
        public async Task<ActionResult> PostFile()
        {
            var file = Request.Form.Files[0];
            if(file == null || file.Length == 0){
            return Content("file not selected");
            }
 
            
           var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot");
                if(!Directory.Exists(path)){
                Directory.CreateDirectory(path);
            }            
            path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"),file.FileName);
            System.Console.WriteLine(path);
          using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            
            }
            
            Models.File f = new Models.File();
            f.name = file.Name;
            f.size = Convert.ToInt32(file.Length);
            f.folderId = 0;
            f.path =path;
            _context.files.Add(f);
            _context.SaveChanges();

            return StatusCode(200);
        }

        [HttpPut("File/{id}")]
        public async Task<ActionResult<IEnumerable<Models.File>>> PutFile(long id, Models.File file)
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
        public async Task<ActionResult<IEnumerable<Models.File>>> DeleteFile(long id)
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
