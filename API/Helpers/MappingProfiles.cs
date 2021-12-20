using Core.Dtos.FleetCarRequest;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<FleetCarRequest, FleetCarRequestDto>()
                .ForMember(d => d.SrvTpDsc, o => o.MapFrom(s => s.SrvTp.Dsc))                ;
        }
    }
}
