using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Infrastructure;

internal sealed class FavoriteProductsContext(
    DbContextOptions<FavoriteProductsContext> options
) : DbContext(options)
{

}
