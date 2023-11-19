using CleanDDDTemplate.Application.Enums;
using CleanDDDTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanDDDTemplate.Application.Context
{
    public interface ICleanDDDTemplateDbContext
    {
        public DbSet<DemoModel> Demo { get; set; }

        public int SaveChanges();

        //A method that gets all database data for cache service implementation.
        public Dictionary<DomainEntityEnum, object> GetAllData();
    }
}