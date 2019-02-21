using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroppboxApi.Models
{
    public class File
    {
        
        public long id{get;set;}
        public string name{get;set;}
        public string typ{get;set;}
        public string path{get;set;}
        public int size{get;set;}
         [ForeignKey("Folder")]
        public long folderId{get;set;}
    }
}