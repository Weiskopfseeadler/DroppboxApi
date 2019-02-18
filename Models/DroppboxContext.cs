using System;
using Microsoft.EntityFrameworkCore;
using DroppboxApi.Controllers;


namespace DroppboxApi.Models
{
    public class DroppboxContext : DbContext
    {
        public DroppboxContext(DbContextOptions<DroppboxContext> options)
            : base(options)
        {
        }
        public DbSet<Organization> organizations { get; set; }
        public DbSet<File>         files         {get;set;}
        public DbSet<User>         users         {get;  set;}
        public DbSet<Folder>       folders       {get;set;}
        public DbSet<User_Organisation>       user_organisation       {get;set;}


        public static implicit operator DroppboxContext(DroppboxController v)
        {
            throw new NotImplementedException();
        }
    }
}