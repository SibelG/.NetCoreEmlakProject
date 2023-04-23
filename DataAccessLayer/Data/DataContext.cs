using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class DataContext:IdentityDbContext<UserAdmin>
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options) { }   
        public DbSet<Advert>Adverts { get; set; }
        public DbSet<City>Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get;  set; }
        public DbSet<Situation> Situations { get; set;}
        public DbSet<EntityLayer.Entities.Type> Types { get; set; }
        public DbSet<UserAdmin> UserAdmins { get; set; }



    }
}
