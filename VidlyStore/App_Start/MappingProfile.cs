using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VidlyStore.api;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MemberShipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<Rental, RentalDto>();
            
            Mapper.CreateMap<CustomerDto, Customer>().
                ForMember(c=>c.Id,opt =>opt.Ignore());
            Mapper.CreateMap<MovieDto, Movie>().
                ForMember(c=>c.Id,opt=>opt.Ignore());
        }
    }
}