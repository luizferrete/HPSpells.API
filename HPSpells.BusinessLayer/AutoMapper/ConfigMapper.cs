using AutoMapper;
using HPSpells.DomainLayer.DTOs;
using HPSpells.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.BusinessLayer.AutoMapper
{
    public class ConfigMapper : Profile
    {
        public ConfigMapper()
        {
            CreateMap<Spell, SpellResponse>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));

            CreateMap<SpellRequest, Spell>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Spell))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Use));
        }
    }
}
