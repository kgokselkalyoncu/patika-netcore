using BookStoreWebApi.Common;

namespace BookStoreWebApi.Models;

public class BooksViewModel
{
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public int PageCount { get; set; }
    public string PublishDate { get; set; } = "";

}