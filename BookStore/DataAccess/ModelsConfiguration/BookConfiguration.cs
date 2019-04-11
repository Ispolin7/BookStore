using BookStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess.ModelsConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder.Property(b => b.PostalCode).IsRequired();
            //builder.Property(a => a.Country).IsRequired();
            //builder.Property(a => a.City).IsRequired();
            //builder.Property(a => a.StreetLine1).IsRequired();
            builder.ToTable("Books");
            
            builder.Property(b => b.SoftDeleted).HasDefaultValue(false);
            builder.Property(b => b.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}
