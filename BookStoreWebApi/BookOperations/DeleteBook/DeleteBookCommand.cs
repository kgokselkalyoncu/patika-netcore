using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.Models;
using BookStoreWebApi.Common;

namespace BookStoreWebApi.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(int id)
    {
        Book? book = _dbContext.Books.SingleOrDefault(x => x.Id == id);

        if(book is null)
            throw new InvalidOperationException("Kitap yok");

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}