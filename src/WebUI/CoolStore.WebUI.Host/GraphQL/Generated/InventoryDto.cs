using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class InventoryDto
        : IInventoryDto
    {
        public InventoryDto(
            System.Guid id, 
            string website, 
            string location, 
            string description)
        {
            Id = id;
            Website = website;
            Location = location;
            Description = description;
        }

        public System.Guid Id { get; }

        public string Website { get; }

        public string Location { get; }

        public string Description { get; }
    }
}
