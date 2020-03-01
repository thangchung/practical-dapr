using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.Data;

namespace CoolStore.ProductCatalogApi.Persistence
{
    public class ProductCatalogDbContext : AppDbContextBase
    {
        public ProductCatalogDbContext(DbContextOptions options) 
            : base(options)
        {
        }
    }
}