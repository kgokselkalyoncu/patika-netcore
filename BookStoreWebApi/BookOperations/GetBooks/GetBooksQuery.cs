using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using BookStoreWebApi.Models;
using BookStoreWebApi.Common;
using AutoMapper;

namespace BookStoreWebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public int BookId {get; set;}
    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        List<Book> bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();   
        List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
        
        return vm;
    }

    public BooksViewModel HandleGetById()
    {
        Book? book = _dbContext.Books.Find(this.BookId);
        
        if(book is null)
            throw new InvalidOperationException("Kitap bulunamadı.");

        BooksViewModel vm = _mapper.Map<BooksViewModel>(book);

        return vm;
    }
}