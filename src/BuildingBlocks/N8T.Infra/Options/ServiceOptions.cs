namespace N8T.Infrastructure.Options
{
    public class ServiceOptions
    {
        public ServiceConfig GraphApi { get; set; }
        public ServiceConfig IdentityService { get; set; }
        public ServiceConfig ProductCatalogService { get; set; }
        public ServiceConfig InventoryService { get; set; }
        public ServiceConfig ShoppingCartService { get; set; }
    }
}