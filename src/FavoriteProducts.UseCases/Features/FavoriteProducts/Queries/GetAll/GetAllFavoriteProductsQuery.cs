using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;
using FluentValidation;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetAll;

public sealed record GetAllFavoriteProductsQuery(Guid CustomerId, int Page, int PageSize) : ICachedQuery<IEnumerable<FavoriteProductDto>>
{
    public string CacheKey => nameof(GetAllFavoriteProductsQuery);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}

public sealed class GetAllFavoriteProductsQueryValidator : AbstractValidator<GetAllFavoriteProductsQuery>
{
    public GetAllFavoriteProductsQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}

public sealed class GetAllFavoriteProductsQueryHandler(
    IRepository<FavoriteProduct> favoriteProductRepository
) : IQueryHandler<GetAllFavoriteProductsQuery, IEnumerable<FavoriteProductDto>>
{
    public async Task<Result<IEnumerable<FavoriteProductDto>>> Handle(
        GetAllFavoriteProductsQuery request, 
        CancellationToken cancellationToken)
        {
            var favoriteProducts = await favoriteProductRepository.GetManyAsync(
                new GetAllFavoriteProductsSpecification(
                    request.CustomerId, 
                    request.Page, 
                    request.PageSize), 
                cancellationToken);

            return Result.Ok(favoriteProducts.Select(FavoriteProductDto.FromEntity));
        }
        
}