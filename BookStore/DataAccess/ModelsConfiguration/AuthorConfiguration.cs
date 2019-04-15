using BookStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.ModelsConfiguration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}
