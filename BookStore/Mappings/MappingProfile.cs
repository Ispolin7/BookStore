using AutoMapper;
using BookStore.Controllers.RequestModels;
using BookStore.Controllers.ViewModels;
using BookStore.DataAccess.Models;
using System.Linq;

namespace BookStore.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Request to DB mapping.
            CreateMap<AuthorRequest, Author>();            
            CreateMap<BookRequest, Book>();                     
            CreateMap<OrderRequest, Order>();            
            CreateMap<LineItemRequest, LineItem>();           
            CreateMap<ReviewRequest, Review>();
            
            //DB to View mapping
            CreateMap<Author, AuthorViewModel>()
                .ForMember(v => v.Books, opt => opt.MapFrom(a => a.BookAuthors.Select(ba => ba.Book).ToList()));
            CreateMap<Book, BookViewModel>();                
            CreateMap<LineItem, LineItemViewModel>();
            CreateMap<Order, OrderViewModel>()
                .ForMember(v => v.DateOrderredUtc, opt => opt.MapFrom(x => x.CreatedAT.ToString()));
            CreateMap<Review, ReviewViewModel>();
        }
    }
}
