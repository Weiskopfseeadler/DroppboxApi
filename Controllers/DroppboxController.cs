using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroppboxApi.Models;

namespace DroppboxApi.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class DroppboxController : ControllerBase
    {
        private readonly DroppboxContext _context;

        public DroppboxController(DroppboxContext context)
        {
            System.Console.WriteLine("Hello");
            _context = context;
           System.Console.WriteLine("Hello2");
        
        
            if (_context.organizations.Count() == 0)
            {
                _context.organizations.Add(new Organization
                {
                    name = "O1",
                });
                _context.SaveChanges();
            }

            if (_context.files.Count() == 0)
            {
                _context.files.Add(new File
                {
                    name = "F1",
                    organizationId= 1,
                    path = "Test",
                    typ = ".txt"
                });
                _context.SaveChanges();
            }
            if (_context.users.Count() == 0)
            {
                _context.users.Add(new User
                {
                    name = "F1",
                    password = "gibbiX12345",
                });
                _context.SaveChanges();
            }
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

       [HttpGet("Organization")]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            return await _context.organizations.ToListAsync();
        }

        [HttpGet("Organization/{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(long id)
        {
            var Organization = await _context.organizations.FindAsync(id);

            if (Organization == null)
            {
                return NotFound();
            }

            return Organization;
        }

        [HttpPost("Organization")]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            _context.organizations.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizations", new { id = organization.id }, organization);
        }

        [HttpPut("Organization/{id}")]
        public async Task<ActionResult<IEnumerable<Organization>>> PutOrganization(long id, Organization organization)
        {
            if (id != organization.id)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.organizations.ToListAsync();
        }

        [HttpDelete("Organization/{id}")]
        public async Task<ActionResult<IEnumerable<Organization>>> DeleteOrganizations(long id)
        {
            var organization = await _context.organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            
            _context.organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return await _context.organizations.ToListAsync();
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
//Postman Test OK
        [HttpPost("ValidationUser")]
        public async Task<ActionResult<User>> ValidationUser(User user)
        {
            bool exist = false;
            foreach (var item in _context.users)
            {
                if (item.name == user.name && item.password == user.password)
                {
                    return await _context.users.FindAsync(item.id);
                }    
            }  
            User error = new User(){
                name = "error"
            };
            return  error;
            

        }
//Postman Test OK
        [HttpPost("ValidationOrganisation")]
        public async Task<ActionResult<User>> ValidationOrganisation(User user)
        {
            bool exist = false;
            foreach (var item in _context.users)
            {
                if (item.name == user.name && item.password == user.password)
                {
                    return await _context.users.FindAsync(item.id);
                }    
            }  
            User error = new User(){
                name = "error"
            };
            return  error;
            

        }
//Postman Test OK
        [HttpGet("OrganisationsOfUser/{id}")]
        public async Task<ActionResult<IEnumerable<Organization>>> OrganisationsOfUser( long id)
        {
            List<Organization> orgList=new List<Organization>();
            
            foreach (var item in _context.user_organisation.Where( t=> t.userId == Convert.ToInt64(id)))
            {
                Console.WriteLine(item);
                Console.WriteLine("<3");
                Console.WriteLine(_context.organizations.FindAsync(item.organisationId));
                orgList.Add(await _context.organizations.FindAsync(item.organisationId));
                
                
            }
            IEnumerable<Organization> orgEnumerabel = orgList;
            
            return orgList;           
        }
//Postman Test OK
        [HttpGet("FilesOfFolder/{id}")]
        public async Task<ActionResult<IEnumerable<File>>> FilesOfFolder( long id)
        {
            List<File> FileList=new List<File>();
            
            foreach (var item in _context.files.Where( t=> t.organizationId == Convert.ToInt64(id)))
            {
                FileList.Add(item);                
                
            }
            
            
            return FileList;           
        }
//Postman Test OK
        [HttpGet("FolderOfFolder/{id}")]
        public async Task<ActionResult<IEnumerable<Folder>>> FolderOfFolder( long id)
        {
            List<Folder> FolderList=new List<Folder>();
            
            foreach (var item in _context.folders.Where( t=> t.folerId == Convert.ToInt64(id)))
            {
                FolderList.Add(item);                
                
            }
            
            
            return FolderList;           
        }
    }

}