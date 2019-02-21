
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
    public class DataController : ControllerBase
    {
        private readonly DroppboxContext _context;

        public DataController(DroppboxContext context)
        {
            _context = context;
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
        public async Task<ActionResult<IEnumerable<Models.File>>> FilesOfFolder( long id)
        {
            List<Models.File> FileList=new List<Models.File>();
            
            foreach (var item in _context.files.Where( t=> t.folderId == Convert.ToInt64(id)))
            {
                FileList.Add(item);                
                
            }
            
            
            return FileList;           
        }
//Postman Test OK
        [HttpGet("FoldersOfFolder/{id}")]
        public async Task<ActionResult<IEnumerable<Folder>>> FolderOfFolder( long id)
        {
            List<Folder> FolderList=new List<Folder>();
            
            foreach (var item in _context.folders.Where( t=> t.folerId == Convert.ToInt64(id)))
            {
                FolderList.Add(item);                
                
            }
            
            
            return FolderList;           
        }

        [HttpGet("FoldersOfOrganisation/{id}")]
        public async Task<ActionResult<IEnumerable<Folder>>> FolderOfOrganisation( long id)
        {
            List<Folder> FolderList=new List<Folder>();
            
            foreach (var item in _context.folders.Where( t=> t.folerId == Convert.ToInt64(id)))
            {
                FolderList.Add(item);                
                
            }
            
            
            return  FolderList;           
        }
    
    }
}
