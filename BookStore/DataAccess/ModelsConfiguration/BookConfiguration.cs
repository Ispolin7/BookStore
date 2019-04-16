using BookStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.ModelsConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.Property(b => b.Title).IsRequired().HasMaxLength(70);
            builder.Property(b => b.Description).IsRequired().HasMaxLength(3000);
            builder.Property(b => b.PublishedOn).IsRequired().HasMaxLength(4);
            builder.Property(b => b.Publisher).IsRequired().HasMaxLength(100);
            builder.Property(b => b.OrgPrice).IsRequired().HasMaxLength(10);
            builder.Property(b => b.ActualPrice).IsRequired().HasMaxLength(10);
            builder.Property(b => b.PromotionalText).IsRequired().HasMaxLength(1000);
            builder.Property(b => b.ImageUrl).IsRequired().HasMaxLength(1000);            
            builder.Property(b => b.SoftDeleted).HasDefaultValue(false);
            builder.Property(b => b.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}
