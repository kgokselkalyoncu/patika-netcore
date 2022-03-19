using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.Models;
using BookStoreWebApi.Common;

namespace BookStoreWebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;

    public GetBooksQuery(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<BooksViewModel> Handle()
    {
        List<Book> bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();

        List<BooksViewModel> vm = new List<BooksViewModel>();

        foreach(Book book in bookList)
        {
            vm.Add(new BooksViewModel(){
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd.MM.yyyy"),
                PageCount = book.PageCount
            });
        }

        return vm;
    }

    public BooksViewModel Handle(int id)
    {
        Book? book = _dbContext.Books.Find(id);
        
        if(book is null)
            throw new InvalidOperationException("Kitap bulunamadı.");

        BooksViewModel vm = new BooksViewModel(){ 
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PublishDate = book.PublishDate.Date.ToString("dd.MM.yyyy"),
            PageCount = book.PageCount
            };

        return vm;
    }
}