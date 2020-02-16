using AutoMapper;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Dtos.User;
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

            CreateMap<ActivityDto, Activity>(MemberList.None);
            CreateMap<Activity, ActivityDto>(MemberList.None);
            
            CreateMap<AfterAutentificationDto, User>(MemberList.None);
            CreateMap<User, AfterAutentificationDto>(MemberList.None);
        }
    }
}
