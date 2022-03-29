using FluentValidation;

namespace BookStoreWebApi.BookOperations.GetBooks;

public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
{
    public GetBooksQueryValidator(){
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}