using AutoMapper;
using backend.DAL.Models;
using backend.DTOs;

namespace backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Question, QuestionDTO>();
            CreateMap<QuestionDTO, Question>();

            CreateMap<Answer, AnswerDTO>();
            CreateMap<AnswerDTO, Answer>();

            CreateMap<Bench, BenchDTO>();
            CreateMap<BenchDTO, Bench>();

            CreateMap<Conversation, ConversationDTO>();
            CreateMap<ConversationDTO, Conversation>();

            CreateMap<History, HistoryDTO>();
            CreateMap<HistoryDTO, History>();

            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, Location>();

            CreateMap<Status, StatusDTO>();
            CreateMap<StatusDTO, Status>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

        }
    }
}
