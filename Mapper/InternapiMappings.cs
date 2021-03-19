using AutoMapper;
using internapi.Model;

namespace internapi.Mapper
{
    public class InternapiMappings : Profile
    {
        public InternapiMappings()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Manager, ManagerDto>().ReverseMap();
            CreateMap<Internship, InternshipDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
        
    }
}