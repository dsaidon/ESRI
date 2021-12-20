using Core.Dtos.FleetCarRequest;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    //FleetCarRequest do hold Image but if it had  the conversion would look like this
    public class ImageUrlResolver : IValueResolver<FleetCarRequest, FleetCarRequestDto, string>
    {
        private readonly IConfiguration _config;
        public ImageUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(FleetCarRequest source, FleetCarRequestDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.SubDrvNm)) //PictureUrl
            {
                return _config["ApiUrl"] + source.SubDrvNm; //.PictureUrl
            }
            return null;
        }
    }
}