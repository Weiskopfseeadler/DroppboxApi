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
    public class OrganisationController : ControllerBase
    {
        private readonly DroppboxContext _context;

        public OrganisationController(DroppboxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            return await _context.organizations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(long id)
        {
            var Organization = await _context.organizations.FindAsync(id);

            if (Organization == null)
            {
                return NotFound();
            }

            return Organization;
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            _context.organizations.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizations", new { id = organization.id }, organization);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
    }
}
