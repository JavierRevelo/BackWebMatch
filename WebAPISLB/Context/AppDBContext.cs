
using Microsoft.EntityFrameworkCore;
using System;
using WebAPISLB.Models;

namespace WebAPISLB.Context

{
    public class AppDBContext: DbContext
    {
       public AppDBContext(DbContextOptions<AppDBContext> options): base(options) { }

        public DbSet<CartaModel> Cartas { get; set; }
        public DbSet<GeneralInformation> Generals{ get; set; }
        public DbSet<CompleteInformationSheet> Completes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartaModel>(entity =>
            {
                entity.ToTable("TEST_TABLE2");
            });
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GeneralInformation>(entity =>
            {
                entity.ToTable("TEST_TABLE3");
            });
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompleteInformationSheet>(entity =>
            {
                entity.ToTable("TEST_TABLE5");
                entity.HasKey(p => p.Id);
            });
        }

    }
}
