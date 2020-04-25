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
    public partial class UpdateProductMutationResultParser
        : JsonResultParserBase<IUpdateProductMutation>
    {
        private readonly IValueSerializer _uuidSerializer;
        private readonly IValueSerializer _stringSerializer;

        public UpdateProductMutationResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _uuidSerializer = serializerResolver.Get("Uuid");
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override IUpdateProductMutation ParserData(JsonElement data)
        {
            return new UpdateProductMutation
            (
                ParseUpdateProductMutationUpdateProduct(data, "updateProduct")
            );

        }

        private global::CoolStore.WebUI.Host.ICatalogProductDto2 ParseUpdateProductMutationUpdateProduct(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new CatalogProductDto2
            (
                DeserializeUuid(obj, "id"),
                DeserializeString(obj, "name")
            );
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
    }
}
