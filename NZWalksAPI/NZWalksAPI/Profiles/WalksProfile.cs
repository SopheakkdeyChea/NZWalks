using AutoMapper;
using NZWalksAPI.Models.DTO.WalkDifficulties;
using NZWalksAPI.Models.DTO.Walks;

namespace NZWalksAPI.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Walk>()
                .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, WalkDifficulty>()
                            .ReverseMap();
        }
    }
}
