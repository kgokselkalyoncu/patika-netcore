using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.Models;
using BookStoreWebApi.Common;
using AutoMapper;

namespace BookStoreWebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateBookModel Model {get; set;} = default!;

    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        Book? book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if(book is not null)
            throw new InvalidOperationException("Kitap zaten mevcut");

        book = _mapper.Map<Book>(Model);

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }
}