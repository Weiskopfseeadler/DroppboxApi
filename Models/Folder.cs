using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroppboxApi.Models
{
    public class Folder
    {
        public long id{get;set;}
        public string name{get;set;}
        [ForeignKey("Folder")]
        public long  folerId{get;set;}

        [ForeignKey("Organization")]
        public long  folderId{get;set;}

        //public ICollection<File> Files {get;set;}
    }
}