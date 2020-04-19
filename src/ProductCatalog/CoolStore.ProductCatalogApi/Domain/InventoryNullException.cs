using N8T.Domain;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class InventoryNullException : CoreException
    {
        public InventoryNullException() : base("Inventory is null.")
        {
        }
    }
}
