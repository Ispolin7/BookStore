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

            builder.ToTable("Books");
            // TODO Book Model configuration
            builder.Property(b => b.SoftDeleted).HasDefaultValue(false);
            builder.Property(b => b.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}
