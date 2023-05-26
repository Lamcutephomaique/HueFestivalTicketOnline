using AutoMapper;
using HueFestivalTicketOnline.Dto;
using HueFestivalTicketOnline.Models;

namespace HueFestivalTicketOnline.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<About, AboutDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<SubMenu, SubMenuDto>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<TicketLocation, TicketLocationDto>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ProgramType, ProgramTypeDto>().ReverseMap();
            CreateMap<TicketType, TicketTypeDto>().ReverseMap();
        }
    }
}
