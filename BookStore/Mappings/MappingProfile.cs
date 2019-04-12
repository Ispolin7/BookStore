using AutoMapper;
using BookStore.Controllers.ValidationModels;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<CustomerValidation, CustomerServiceModel>()
            //    .ForMember(d => d.FirstName, s => s.MapFrom(i => i.Name));
            //CreateMap<CustomerServiceModel, CustomerValidation>()
            //    .ForMember(d => d.Name, s => s.MapFrom(i => i.FirstName));

            //CreateMap<Services.Models.Customer, Repositories.Models.Customer>().ReverseMap();

            CreateMap<AuthorCreateModel, Author>();
            CreateMap<AuthorUpdateModel, Author>();

            CreateMap<BookCreateModel, Book>();
            CreateMap<BookUpdateModel, Book>();

            CreateMap<OrderCreateModel, Order>();
            CreateMap<OrderUpdateModel, Order>();

            CreateMap<ReviewCreateModel, Review>();
            CreateMap<ReviewUpdateModel, Review>();

            CreateMap<Author, AuthorViewModel>();
            CreateMap<Book, BookViewModel>();
            CreateMap<LineItem, LineItemViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<Review, ReviewViewModel>();
        }
    }
}
