using AutoMapper;
using SocialMedia.Domain.Entities.PostAggregate.Events;
using SocialMedia.Domain.QueriesModel;

namespace SocialMedia.Domain.Profiles;

public class EventToQueryModelProfile : Profile
{
    public EventToQueryModelProfile()
    {
        CreateMap<PostCreatedEvent, PostQueryModel>(MemberList.Destination);
        CreateMap<PostEditedEvent, PostQueryModel>(MemberList.Destination);
        CreateMap<PostDeletedEvent, PostQueryModel>(MemberList.Destination);
    }
}