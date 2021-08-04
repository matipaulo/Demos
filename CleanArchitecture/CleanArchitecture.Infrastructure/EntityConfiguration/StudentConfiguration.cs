using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.EntityConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasIndex(x => x.Name);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(x => x.Age).IsRequired();
        }
    }
}