using AutoMapper;
using Web.Models;
using Web.ViewModels;

namespace Web;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Player, PlayerViewModel>().ReverseMap();
    }
}