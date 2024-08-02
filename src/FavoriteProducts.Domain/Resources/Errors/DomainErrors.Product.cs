using FavoriteProducts.Domain.Core.Results;
using ProductEntity = FavoriteProducts.Domain.Resources.Products.Product;

namespace FavoriteProducts.Domain.Resources.Errors;

public static partial class DomainErrors
{
    public static class Product
    {
        public static readonly DomainError<ProductEntity> NotFound = new("product_not_found", "Product not found.");
        public static readonly DomainError<ProductEntity> InvalidProduct = new("invalid_product", "Product can't have invalid state");
        public static readonly DomainError<ProductEntity> InvalidImageUrl = new("invalid_image_url", "Product can't have invalid image url");
        public static readonly DomainError<ProductEntity> InvalidProductPrice = new("invalid_product_price", "Product can't have invalid price");
        public static readonly DomainError<ProductEntity> InvalidProductDescription = new("invalid_product_description", "Product can't have invalid description");
        public static readonly DomainError<ProductEntity> InvalidProductBrand = new("invalid_product_brand", "Product can't have invalid brand");
        public static readonly DomainError<ProductEntity> InvalidProductTitle = new("invalid_product_title", "Product can't have invalid title");
        public static readonly DomainError<ProductEntity> CreateProductFailed = new("create_product_failed", "Failed to create product.");
        public static readonly DomainError<ProductEntity> UpdateProductFailed = new("update_product_failed", "Failed to update product.");
        public static readonly DomainError<ProductEntity> DeleteProductFailed = new("delete_product_failed", "Failed to delete product.");
        public static readonly DomainError<ProductEntity> InvalidProductReviewScore = new("invalid_product_review_score", "Product can't have invalid review score");
        public static readonly DomainError<ProductEntity> CannotDecreaseReviewScore = new("cannot_decrease_review_score", "Product review score can't be decreased");
    }
}