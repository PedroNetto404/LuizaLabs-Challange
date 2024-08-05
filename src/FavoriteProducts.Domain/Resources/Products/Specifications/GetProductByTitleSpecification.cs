using Ardalis.Specification;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;

namespace FavoriteProducts.Domain.Resources.Products.Specifications;

public sealed class GetProductByTitleSpecification : SingleResultSpecification<Product>
{
    public GetProductByTitleSpecification(ProductTitle title) => 
        Query
        .Where(product => product.Title == title);
}