using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WDCApp.Entities
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Development> Development { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<TypeOfService> TypeOfService { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Contract)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.IdClient);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Contract)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.IdService);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Development)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfService>()
                .HasMany(e => e.Service)
                .WithRequired(e => e.TypeOfService)
                .HasForeignKey(e => e.IdTypeOfService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Development)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
