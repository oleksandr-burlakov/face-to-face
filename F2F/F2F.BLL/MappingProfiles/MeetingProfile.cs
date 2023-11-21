using AutoMapper;
using F2F.BLL.Models.Meetings;
using F2F.DLL.Entities;

namespace F2F.BLL.MappingProfiles;

internal class MeetingProfile : Profile
{
    public MeetingProfile()
    {
        CreateMap<Meeting, MeetingModel>();
        CreateMap<AddMeetingModel, Meeting>();
        CreateMap<UpdateMeetingModel, Meeting>();
    }
}
