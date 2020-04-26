using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using StrawberryShake;
using StrawberryShake.Configuration;
using StrawberryShake.Http;
using StrawberryShake.Http.Subscriptions;
using StrawberryShake.Transport;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetProductsResultParser
        : JsonResultParserBase<IGetProducts>
    {
        private readonly IValueSerializer _intSerializer;
        private readonly IValueSerializer _uuidSerializer;
        private readonly IValueSerializer _stringSerializer;
        private readonly IValueSerializer _floatSerializer;

        public GetProductsResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _intSerializer = serializerResolver.Get("Int");
            _uuidSerializer = serializerResolver.Get("Uuid");
            _stringSerializer = serializerResolver.Get("String");
            _floatSerializer = serializerResolver.Get("Float");
        }

        protected override IGetProducts ParserData(JsonElement data)
        {
            return new GetProducts
            (
                ParseGetProductsProducts(data, "products")
            );

        }

        private global::CoolStore.WebUI.Host.IOffsetPagingOfCatalogProductDto ParseGetProductsProducts(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return new OffsetPagingOfCatalogProductDto
            (
                ParseGetProductsProductsEdges(obj, "edges"),
                DeserializeInt(obj, "totalCount")
            );
        }

        private global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.ICatalogProductDto> ParseGetProductsProductsEdges(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            int objLength = obj.GetArrayLength();
            var list = new global::CoolStore.WebUI.Host.ICatalogProductDto[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new CatalogProductDto
                (
                    DeserializeUuid(element, "id"),
                    DeserializeString(element, "name"),
                    DeserializeString(element, "imageUrl"),
                    DeserializeFloat(element, "price"),
                    DeserializeNullableString(element, "description"),
                    ParseGetProductsProductsEdgesCategory(element, "category"),
                    ParseGetProductsProductsEdgesInventory(element, "inventory")
                );

            }

            return list;
        }

        private global::CoolStore.WebUI.Host.ICategoryDto ParseGetProductsProductsEdgesCategory(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new CategoryDto
            (
                DeserializeUuid(obj, "id"),
                DeserializeString(obj, "name")
            );
        }

        private global::CoolStore.WebUI.Host.IInventoryDto ParseGetProductsProductsEdgesInventory(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new InventoryDto
            (
                DeserializeUuid(obj, "id"),
                DeserializeString(obj, "website"),
                DeserializeString(obj, "location"),
                DeserializeNullableString(obj, "description")
            );
        }

        private int DeserializeInt(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (int)_intSerializer.Deserialize(value.GetInt32());
        }
        private System.Guid DeserializeUuid(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (System.Guid)_uuidSerializer.Deserialize(value.GetString());
        }

        private string DeserializeString(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_stringSerializer.Deserialize(value.GetString());
        }

        private double DeserializeFloat(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (double)_floatSerializer.Deserialize(value.GetDouble());
        }

        private string DeserializeNullableString(JsonElement obj, string fieldName)
        {
            if (!obj.TryGetProperty(fieldName, out JsonElement value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return (string)_stringSerializer.Deserialize(value.GetString());
        }
    }
}
