using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.ViewModels;

namespace System_Rezerwacji_Biletów.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, UserViewModel>();
            CreateMap<UserViewModel, IdentityUser>();

            CreateMap<Event, EventViewModel>();
            CreateMap<EventViewModel, Event>();
        }
    }
}


