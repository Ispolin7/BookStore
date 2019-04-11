using AutoMapper;
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
        }
    }
}
