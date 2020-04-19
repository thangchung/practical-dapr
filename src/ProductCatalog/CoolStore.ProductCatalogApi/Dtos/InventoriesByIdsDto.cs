using System.Collections.Generic;

namespace CoolStore.ProductCatalogApi.Dtos
{
    public class InventoriesByIdsDto
    {
        public List<string> Ids { get; set; } = new List<string>();
    }
}
