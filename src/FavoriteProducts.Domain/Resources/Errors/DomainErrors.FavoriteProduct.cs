using FavoriteProducts.Domain.Core.Results;
using FavoriteProductEntity = FavoriteProducts.Domain.Resources.FavoriteProducts.FavoriteProduct;

namespace FavoriteProducts.Domain.Resources.Errors;

public static partial class DomainErrors
{
    public static class FavoriteProduct
    {
        public static readonly DomainError<FavoriteProductEntity> NotFound = new("favorite_product_not_found", "Favorite product not found.");
        public static readonly DomainError<FavoriteProductEntity> CannotFavoriteInactiveProduct = new("cannot_favorite_inactive_product", "Cannot favorite inactive product.");
        public static readonly DomainError<FavoriteProductEntity> NotSaved = new("favorite_product_not_saved", "Favorite product not saved.");
        public static readonly DomainError<FavoriteProductEntity> NotDeleted = new("favorite_product_not_deleted", "Favorite product not deleted.");
    }
}