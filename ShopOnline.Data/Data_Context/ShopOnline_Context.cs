using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Data.Data_Context
{
   public  class ShopOnline_Context:DbContext
    {
        public ShopOnline_Context(DbContextOptions<ShopOnline_Context> options):base(options) { }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Person>().HasKey(x => x.PersonId);
            //modelBuilder.Entity<Person>().ToTable("Person");
            //modelBuilder.Entity<Officer>().HasKey<int>(x => x.OfficerId);
            //modelBuilder.Entity<Officer>().ToTable("Officer");

        }
        DbSet<Product> products { get; set; }
        DbSet<Category> Categories{ get; set; }


    }
}
