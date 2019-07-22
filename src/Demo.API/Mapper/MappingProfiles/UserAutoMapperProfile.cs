

namespace Demo.API.Mapper.MappingProfiles
{
    using System;
    using AutoMapper;
    using Demo.Entities;
    using Demo.API.Models;

    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<User, UserModel>();
        }
    }
}
