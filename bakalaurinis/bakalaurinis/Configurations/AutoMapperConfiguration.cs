using AutoMapper;
using bakalaurinis.Dtos.Activity;
using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Dtos.Message;
using bakalaurinis.Dtos.User;
using bakalaurinis.Dtos.UserSettings;
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

            CreateMap<MessageDto, Message>(MemberList.None);
            CreateMap<Message, MessageDto>(MemberList.None);

            CreateMap<UserSettingsDto, UserSettings>(MemberList.None);
            CreateMap<UserSettings, UserSettingsDto>(MemberList.None);

            CreateMap<UserSettingsDto, UserSettings>(MemberList.None);
            CreateMap<UserSettings, UserSettingsDto>(MemberList.None);

            CreateMap<NewInvitationDto, Invitation>(MemberList.None);
            CreateMap<Invitation, NewInvitationDto>(MemberList.None);

            CreateMap<UpdateInvitationDto, Invitation>(MemberList.None);
            CreateMap<Invitation, UpdateInvitationDto>(MemberList.None);

            CreateMap<InvitationDto, Invitation>(MemberList.None);
            CreateMap<Invitation, InvitationDto>(MemberList.None);
        }
    }
}
