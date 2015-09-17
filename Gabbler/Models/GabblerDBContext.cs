using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Gabbler.Models
{
    public class GabblerDBContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Avatar> Avatar { get; set; }
        public DbSet<Background> Background { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Gab> Gab { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Follow>().HasRequired(c => c.UFollow).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Follow>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Gab>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
        }
    }
}