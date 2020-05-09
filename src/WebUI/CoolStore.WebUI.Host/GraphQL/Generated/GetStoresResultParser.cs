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
    public partial class GetStoresResultParser
        : JsonResultParserBase<IGetStores>
    {
        private readonly IValueSerializer _uuidSerializer;
        private readonly IValueSerializer _stringSerializer;

        public GetStoresResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _uuidSerializer = serializerResolver.Get("Uuid");
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override IGetStores ParserData(JsonElement data)
        {
            return new GetStores
            (
                ParseGetStoresStores(data, "stores")
            );

        }

        private global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.IStoreDto1> ParseGetStoresStores(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            int objLength = obj.GetArrayLength();
            var list = new global::CoolStore.WebUI.Host.IStoreDto1[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new StoreDto1
                (
                    DeserializeUuid(element, "id"),
                    DeserializeNullableString(element, "location")
                );

            }

            return list;
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
