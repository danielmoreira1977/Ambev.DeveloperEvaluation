using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsCommand>
{
    public GetProductsValidator()
    {
        RuleFor(x => x.pageNumber)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("The _page parameter must be greater than 0");

        RuleFor(x => x.pageSize)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .WithMessage("The _size parameter must be greater or equal to 1");
    }
}
