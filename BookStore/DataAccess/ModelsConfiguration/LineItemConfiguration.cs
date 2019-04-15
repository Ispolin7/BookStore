using BookStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess.ModelsConfiguration
{
    public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> builder)
        {
            builder.ToTable("LineItems");
            builder.Property(b => b.LineNum).IsRequired().HasMaxLength(10);
            builder.Property(b => b.NumBooks).IsRequired().HasMaxLength(3);
            builder.Property(b => b.BookPrice).IsRequired().HasMaxLength(10);
            builder.Property(b => b.BookId).IsRequired();
            builder.Property(b => b.OrderId).IsRequired();
        }
    }
}
