using AutoMapper;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Player, PlayerViewModel>().ReverseMap();
    }
}