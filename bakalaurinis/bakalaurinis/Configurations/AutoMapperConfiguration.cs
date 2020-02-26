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

            CreateMap<UserNameDto, User>(MemberList.None);
            CreateMap<User, UserNameDto>(MemberList.None);

            CreateMap<RegistrationDto, User>(MemberList.None);
            CreateMap<User, RegistrationDto>(MemberList.None);

            CreateMap<GetScheduleStatus, User>(MemberList.None);
            CreateMap<User, GetScheduleStatus>(MemberList.None);
        }
    }
}
