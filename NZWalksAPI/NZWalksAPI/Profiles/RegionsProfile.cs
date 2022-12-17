using AutoMapper;
using NZWalksAPI.Models.DTO.Regions;

namespace NZWalksAPI.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Region>()
                .ReverseMap();
        }
    }
}
