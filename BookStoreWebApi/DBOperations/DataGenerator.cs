using Microsoft.EntityFrameworkCore;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if(context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                new Book{ Title = "aaaa", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001,06,12) },
                new Book{ Title = "bbbb", GenreId = 1, PageCount = 300, PublishDate = new DateTime(2010,07,05) },
                new Book{ Title = "cccc", GenreId = 1, PageCount = 500, PublishDate = new DateTime(2011,03,19) }
            );

            context.SaveChanges();
        }
    }
}