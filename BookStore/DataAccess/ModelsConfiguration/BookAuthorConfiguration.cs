using BookStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess.ModelsConfiguration
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.ToTable("BookAuthors");
            builder.HasKey(ba => new { ba.BookId, ba.AuthorId });
            //builder.Property(ba => ba.Order).HasConversion(

            //    );
            //builder.Property(b => b.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            //var converter = new ValueConverter<Guid[], string>(
            //    v => v.Select(g => g.ToString()),
            //    v => v.Select(g => Guid.Parse(g));

            //Guid[] guids = new Guid[200];
            //var guidStrings = guids.Select(g => g.ToString());
            //var revertedGuids = guidStrings.Select(g => Guid.Parse(g));

        }
    }
}
