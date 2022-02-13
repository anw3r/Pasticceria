using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pasticceria
{
    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(4,3);
        }
    }
}
