using BookStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.ModelsConfiguration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.Property(r => r.VoterName).IsRequired().HasMaxLength(255);
            builder.Property(r => r.NumStars).IsRequired().HasMaxLength(2);
            builder.Property(r => r.Comment).IsRequired().HasMaxLength(1000);
            builder.Property(r => r.BookId).IsRequired();
            builder.Property(b => b.CreatedAT).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
    }
}
