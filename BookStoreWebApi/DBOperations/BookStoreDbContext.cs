using Microsoft.EntityFrameworkCore;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.DBOperations;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {}

    public DbSet<Book> Books { get; set; } = default!;
}