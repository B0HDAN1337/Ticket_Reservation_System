using AutoMapper;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.ViewModels;

namespace System_Rezerwacji_Biletów.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<Event, EventViewModel>();
            CreateMap<EventViewModel, Event>();
        }
    }
}


