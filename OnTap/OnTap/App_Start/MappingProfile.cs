using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;
using OnTap.Dtos;

namespace OnTap.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Patron, PatronDto>()
            	.ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<PatronDto, Patron>();

            Mapper.CreateMap<Special, SpecialDto>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<SpecialDto, Special>();

            Mapper.CreateMap<TapBeer, TapBeerDto>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<TapBeerDto, TapBeer>();
        }
    }
}