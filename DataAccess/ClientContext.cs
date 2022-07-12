using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ClientContext : IdentityDbContext<User, Role, int>
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(x => x.ToTable("Users"));
            builder.Entity<Role>(x => x.ToTable("Roles"));
        }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TypeEmployment> TypeEmployments { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Applay> Applays { get; set; }
    }

}
