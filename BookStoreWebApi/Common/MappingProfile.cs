using AutoMapper;
using BookStoreWebApi.Models;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile(){
        CreateMap<CreateBookModel,Book>();
        CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
    }
}