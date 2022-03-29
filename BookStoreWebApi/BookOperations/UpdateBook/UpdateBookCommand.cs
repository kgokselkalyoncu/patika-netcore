using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.Models;
using BookStoreWebApi.Common;

namespace BookStoreWebApi.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    public int BookId {get; set;}
    public UpdateBookModel Model {get; set;} = default!;

    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        Book? book = _dbContext.Books.SingleOrDefault(x => x.Id == this.BookId);

        if(book is null)
            throw new InvalidOperationException("Kitap mevcut değil");
        
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
        book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
        book.Title = Model.Title != default ? Model.Title : book.Title;

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }
}