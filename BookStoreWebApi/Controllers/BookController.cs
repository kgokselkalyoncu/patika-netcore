using Microsoft.AspNetCore.Mvc;
using BookStoreWebApi.Entities;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Models;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;


namespace BookStoreWebApi;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery getBooksQuery = new GetBooksQuery(_context, _mapper);
        List<BooksViewModel> result = getBooksQuery.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        GetBooksQuery getBooksQuery = new GetBooksQuery(_context, _mapper);
        BooksViewModel result = new BooksViewModel();
        try{
            result = getBooksQuery.Handle(id);
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }


        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);

        try{

            createBookCommand.Model = newBook;
            CreateBookCommandValidator validations = new CreateBookCommandValidator();
            validations.ValidateAndThrow(createBookCommand);

            createBookCommand.Handle();
            // ValidationResult result = validations.Validate(createBookCommand);

            // if(!result.IsValid){
            //     string errorMessage = "";
            //     foreach (ValidationFailure item in result.Errors)
            //     {
            //         // Console.WriteLine("Özellik :" + item.PropertyName);
            //         // Console.WriteLine("Error Code :" + item.ErrorCode);
            //         // Console.WriteLine("Error Message :" + item.ErrorMessage);

            //         errorMessage += item.ErrorMessage + "\n";
            //     }

            //     return BadRequest(errorMessage);
            // }
            // else{
            //     createBookCommand.Handle();
            // }
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
        DeleteBookCommand deleteBook = new DeleteBookCommand(_context);

        try{
            deleteBook.Handle(id);
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }

        return Ok();
    }

}