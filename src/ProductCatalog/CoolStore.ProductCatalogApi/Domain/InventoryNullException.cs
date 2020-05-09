using N8T.Domain;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class StoreNullException : CoreException
    {
        public StoreNullException() : base("Store is null.")
        {
        }
    }
}
