﻿using FavoriteProducts.Domain.Core.Results;
using ProductEntity = FavoriteProducts.Domain.Resources.Products.Product;

namespace FavoriteProducts.Domain.Resources.Errors;

public static partial class DomainErrors
{
    public static class Product
    {
        public static readonly DomainResult<ProductEntity> NotFound = new("product_not_found", "Product not found.");
        public static readonly DomainResult<ProductEntity> InvalidProduct = new("invalid_product", "Product can't have invalid state");
        public static readonly DomainResult<ProductEntity> ProductAlreadyActive = new("product_already_active", "Product already active.");
        public static readonly DomainResult<ProductEntity> ProductAlreadyInactive = new("product_already_inactive", "Product already inactive.");
        public static readonly DomainResult<ProductEntity> InvalidImageUrl = new("invalid_image_url", "Product can't have invalid image url");
        public static readonly DomainResult<ProductEntity> InvalidProductPrice = new("invalid_product_price", "Product can't have invalid price");
        public static readonly DomainResult<ProductEntity> InvalidProductDescription = new("invalid_product_description", "Product can't have invalid description");
        public static readonly DomainResult<ProductEntity> InvalidProductBrand = new("invalid_product_brand", "Product can't have invalid brand");
        public static readonly DomainResult<ProductEntity> InvalidProductTitle = new("invalid_product_title", "Product can't have invalid title");
        public static readonly DomainResult<ProductEntity> CreateProductFailed = new("create_product_failed", "Failed to create product.");
        public static readonly DomainResult<ProductEntity> UpdateProductFailed = new("update_product_failed", "Failed to update product.");
        public static readonly DomainResult<ProductEntity> DeleteProductFailed = new("delete_product_failed", "Failed to delete product.");
    }
}