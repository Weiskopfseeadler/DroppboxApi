using System;

namespace DroppboxApi.Models
{
    public class File
    {
        
        public long id{get;set;}
        public string name{get;set;}
        public string typ{get;set;}
        public string path{get;set;}
        public int size{get;set;}
        public int organizationId{get;set;}
    }
}