using CleanDDDTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanDDDTemplate.Persistence.Configurations
{
    public class DemoConfiguration : IEntityTypeConfiguration<DemoModel>
    {
        public void Configure(EntityTypeBuilder<DemoModel> builder)
        {
            //Structure And Validations
            //builder.HasKey(demo => demo.PrimaryKey);

            //Nullability
            //builder.Property(demo => demo.Property1).IsRequired();
            //builder.Property(demo => demo.Property2).IsRequired(false);

            //Relations
            //builder.HasOne(demo => demo.ForeignEntity).WithMany(foreignEntity => foreignEntity.Demos).HasForeignKey(demo => demo.ForeignKey);
        }
    }
}