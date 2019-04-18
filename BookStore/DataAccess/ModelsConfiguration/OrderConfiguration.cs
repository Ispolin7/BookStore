using BookStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.ModelsConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(o => o.CustomerName).IsRequired().HasMaxLength(255);
            builder.Property(o => o.ExpectedDeliveryDate).IsRequired();
            builder.Property(b => b.CreatedAT).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
    }
}
