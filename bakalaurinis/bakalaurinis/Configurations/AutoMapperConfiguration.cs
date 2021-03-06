﻿using AutoMapper;
using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Dtos.Message;
using bakalaurinis.Dtos.User;
using bakalaurinis.Dtos.UserInvitations;
using bakalaurinis.Dtos.UserSettings;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Infrastructure.Database.Models;

namespace bakalaurinis.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration() : this("Database") { }

        public AutoMapperConfiguration(string profileName) : base(profileName)
        {
            CreateMap<NewWorkDto, Work>(MemberList.None);
            CreateMap<Work, NewWorkDto>(MemberList.None);

            CreateMap<WorkDto, Work>(MemberList.None);
            CreateMap<Work, WorkDto>(MemberList.None);

            CreateMap<AfterAutenticationDto, User>(MemberList.None);
            CreateMap<User, AfterAutenticationDto>(MemberList.None);

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

            CreateMap<UserInvitationsDto, Invitation>(MemberList.None);
            CreateMap<Invitation, UserInvitationsDto>(MemberList.None);

            CreateMap<UserInvitationsDto, Invitation>(MemberList.None);
            CreateMap<Invitation, UserInvitationsDto>(MemberList.None);

            CreateMap<GetUserItemsPerPageSetting, UserSettings>(MemberList.None);
            CreateMap<UserSettings, GetUserItemsPerPageSetting>(MemberList.None);

            CreateMap<UpdateUserItemsPerPageSettings, UserSettings>(MemberList.None);
            CreateMap<UserSettings, UpdateUserItemsPerPageSettings>(MemberList.None);

            CreateMap<WorkStatusConfirmationDto, Work>(MemberList.None);
            CreateMap<Work, WorkStatusConfirmationDto>(MemberList.None);
        }
    }
}
