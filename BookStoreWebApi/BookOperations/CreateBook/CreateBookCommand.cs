using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.Models;
using BookStoreWebApi.Common;

namespace BookStoreWebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    public CreateBookModel Model {get; set;} = default!;

    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        Book? book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if(book is not null)
            throw new InvalidOperationException("Kitap zaten mevcut");
        
        book = new Book();
        book.Title = Model.Title;
        book.GenreId = Model.GenreId;
        book.PageCount = Model.PageCount;
        book.PublishDate = Model.PublishDate;

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }
}