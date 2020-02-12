using AutoMapper;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Infrastructure.Database.Models;

namespace bakalaurinis.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration() : this("Database") { }

        public AutoMapperConfiguration(string profileName) : base(profileName)
        {
            CreateMap<NewActivityDto, Activity>(MemberList.None);
            CreateMap<Activity, NewActivityDto>(MemberList.None);

            CreateMap<GetActivityDto, Activity>(MemberList.None);
            CreateMap<Activity, GetActivityDto>(MemberList.None);

            CreateMap<UpdateActivityDto, Activity>(MemberList.None);
            CreateMap<Activity, UpdateActivityDto>(MemberList.None);
        }
    }
}
