using Microsoft.AspNetCore.Mvc;
using BookStoreWebApi.Entities;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Models;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.UpdateBook;

namespace BookStoreWebApi;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;

    // private static List<Book> BookList = new List<Book>()
    // {
    //     new Book{
    //         Id = 1,
    //         Title = "Test",
    //         GenreId = 1,
    //         PageCount = 200,
    //         PublishDate = new DateTime(2001,06,12)
    //     },
    //     new Book{
    //         Id = 2,
    //         Title = "aaa",
    //         GenreId = 1,
    //         PageCount = 300,
    //         PublishDate = new DateTime(2010,07,05)
    //     },
    //     new Book{
    //         Id = 3,
    //         Title = "bbb",
    //         GenreId = 1,
    //         PageCount = 500,
    //         PublishDate = new DateTime(2011,03,19)
    //     }
    // };

    public BookController(BookStoreDbContext context){
        _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery getBooksQuery = new GetBooksQuery(_context);
        List<BooksViewModel> result = getBooksQuery.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        GetBooksQuery getBooksQuery = new GetBooksQuery(_context);
        BooksViewModel result = new BooksViewModel();
        try{
            result = getBooksQuery.Handle(id);
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }


        return Ok(result);
    }

    [HttpGet]
    [Route("Get/")]
    public Book? Get([FromQuery] string id)
    {
        Book? book = _context.Books.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        return book;
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand createBookCommand = new CreateBookCommand(_context);

        try{

            createBookCommand.Model = newBook;
            createBookCommand.Handle();

        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
    {
        UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);

        try{

            updateBookCommand.Model = updateBook;
            updateBookCommand.Handle(id);

        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        Book? book = _context.Books.SingleOrDefault(x => x.Id == id);

        if(book is null)
            return BadRequest();

        _context.Books.Remove(book);
        _context.SaveChanges();
        return Ok();
    }

}