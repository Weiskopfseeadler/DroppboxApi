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
    public class DroppboxController : ControllerBase
    {
        private readonly DroppboxContext _context;

        public DroppboxController(DroppboxContext context)
        {
          
            _context = context;
         
        }
    }

}