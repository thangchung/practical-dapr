using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using CoolStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.WebUI.Host.Controllers
{
    [ApiController]
    [Route("api/inventories")]
    public class InventoryController : ControllerBase
    {
        private readonly IGraphQLClient _client;

        public InventoryController(IGraphQLClient client)
        {
            _client = client;
        }

        [Authorize]
        [HttpGet]
        public Task<ImmutableList<KeyValueModel>> GetInventories()
        {
            var result = new List<KeyValueModel>
            {
                new KeyValueModel {Key = new Guid("90C9479E-A11C-4D6D-AAAA-0405B6C0EFCD"), Value = "Vietnam"}
            };

            return Task.FromResult(result.ToImmutableList());
        }
    }
}
