using AutoMapper;
using ContatosAPI.Data.DTOs;
using ContatosAPI.Models;

namespace ContatosAPI.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile() 
        {
            CreateMap<ContactDTO, Contact>().ReverseMap();
        }
    }
}
