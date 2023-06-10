using Microsoft.EntityFrameworkCore;
using NoName.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoName.Data.Context
{
    public class NoNameContext : DbContext
    {
        public NoNameContext(DbContextOptions<NoNameContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        

    }
}
