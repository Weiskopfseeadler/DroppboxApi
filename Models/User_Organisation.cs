using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroppboxApi.Models
{    
    public class User_Organisation
    {
        public long id {get;set;}
        [ForeignKey("User")]
        public long userId {get;set;}
        [ForeignKey("Organization")]
        public long organisationId {get;set;}
    }
}