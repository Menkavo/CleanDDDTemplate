using CleanDDDTemplate.Application.Context;
using CleanDDDTemplate.Application.Enums;
using CleanDDDTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanDDDTemplate.Persistence.Context
{
    internal class CleanDDDTemplateDbContext : DbContext, ICleanDDDTemplateDbContext
    {
        public CleanDDDTemplateDbContext(DbContextOptions<CleanDDDTemplateDbContext> option) : base(option) { }


        public DbSet<DemoModel> Demo { get; set; }


        public override int SaveChanges() => base.SaveChanges();

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

        public Dictionary<DomainEntityEnum, object> GetAllData()
        {
            //Gets all database data.

            var data = new Dictionary<DomainEntityEnum, object>()
            {
                {DomainEntityEnum.Demo, Demo.IgnoreQueryFilters().ToList() }
            };
            return data;
        }
    }
}