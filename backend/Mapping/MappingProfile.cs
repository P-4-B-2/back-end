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
        }
    }
}
