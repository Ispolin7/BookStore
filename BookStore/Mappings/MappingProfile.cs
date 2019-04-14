using AutoMapper;
using BookStore.Controllers.RequestModels;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using System;
using System.Linq;

namespace BookStore.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<AuthorCreateModel, Author>();
            CreateMap<AuthorUpdateModel, Author>();

            CreateMap<BookCreateModel, Book>()
                .ForMember(u => u.PublishedOn, opt => opt.MapFrom(x => DateTime.Parse(x.PublishedOn)));
            CreateMap<BookUpdateModel, Book>()
                .ForMember(u => u.PublishedOn, opt => opt.MapFrom(x => DateTime.Parse(x.PublishedOn)));

            CreateMap<OrderCreateModel, Order>();
            CreateMap<OrderUpdateModel, Order>();

            CreateMap<LineItemCreateModel, LineItem>();
            CreateMap<LineItemUpdateModel, LineItem>();

            CreateMap<ReviewCreateModel, Review>();
            CreateMap<ReviewUpdateModel, Review>();

            CreateMap<Author, AuthorViewModel>()
                .ForMember(v => v.Books, opt => opt.MapFrom(a => a.BookAuthors.Select(ba => ba.Book).ToList()));

            CreateMap<Book, BookViewModel>()
                .ForMember(v => v.PublishedOn, opt => opt.MapFrom(b => b.PublishedOn.ToString()));
                //.ForMember(v => v.Authors, opt => opt.MapFrom(b => b.Authors.Select(a => new {a.Id, a.Name})));

            CreateMap<LineItem, LineItemViewModel>();

            CreateMap<Order, OrderViewModel>()
                .ForMember(v => v.DateOrderredUtc, opt => opt.MapFrom(x => x.CreatedAT.ToString()));

            CreateMap<Review, ReviewViewModel>();
        }
    }
}
