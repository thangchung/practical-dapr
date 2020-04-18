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
    public partial class CreateProductMutationResultParser
        : JsonResultParserBase<ICreateProductMutation>
    {
        private readonly IValueSerializer _uuidSerializer;
        private readonly IValueSerializer _stringSerializer;

        public CreateProductMutationResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _uuidSerializer = serializerResolver.Get("Uuid");
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override ICreateProductMutation ParserData(JsonElement data)
        {
            return new CreateProductMutation
            (
                ParseCreateProductMutationCreateProduct(data, "createProduct")
            );

        }

        private global::CoolStore.WebUI.Host.ICreateProductResponse ParseCreateProductMutationCreateProduct(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new CreateProductResponse
            (
                ParseCreateProductMutationCreateProductProduct(obj, "product")
            );
        }

        private global::CoolStore.WebUI.Host.ICatalogProductDto1 ParseCreateProductMutationCreateProductProduct(
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

            return new CatalogProductDto1
            (
                DeserializeUuid(obj, "id"),
                DeserializeNullableString(obj, "name")
            );
        }

        private System.Guid DeserializeUuid(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (System.Guid)_uuidSerializer.Deserialize(value.GetString());
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
