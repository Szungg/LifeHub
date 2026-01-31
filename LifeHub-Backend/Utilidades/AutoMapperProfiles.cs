using AutoMapper;
using LifeHub.DTOs;
using LifeHub.Models;

namespace LifeHub.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // User mappings
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

            // Friendship mappings
            CreateMap<Friendship, FriendshipDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => (int)s.Status))
                .ReverseMap()
                .ForMember(d => d.Status, o => o.MapFrom(s => (FriendshipStatus)s.Status));

            CreateMap<CreateFriendshipDto, Friendship>();
            CreateMap<UpdateFriendshipDto, Friendship>();

            // Message mappings
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<CreateMessageDto, Message>();

            // Recommendation mappings
            CreateMap<Recommendation, RecommendationDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => (int)s.Type))
                .ReverseMap()
                .ForMember(d => d.Type, o => o.MapFrom(s => (RecommendationType)s.Type));

            CreateMap<CreateRecommendationDto, Recommendation>();
            CreateMap<UpdateRecommendationDto, Recommendation>();

            // Document mappings
            CreateMap<Document, DocumentDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => (int)s.Type))
                .ReverseMap()
                .ForMember(d => d.Type, o => o.MapFrom(s => (DocumentType)s.Type));

            CreateMap<CreateDocumentDto, Document>();
            CreateMap<UpdateDocumentDto, Document>();

            // MusicFile mappings
            CreateMap<MusicFile, MusicFileDto>().ReverseMap();
            CreateMap<CreateMusicFileDto, MusicFile>();
            CreateMap<UpdateMusicFileDto, MusicFile>();
        }
    }
}
