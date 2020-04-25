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
    public partial class GetCategoriesResultParser
        : JsonResultParserBase<IGetCategories>
    {
        private readonly IValueSerializer _uuidSerializer;
        private readonly IValueSerializer _stringSerializer;

        public GetCategoriesResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _uuidSerializer = serializerResolver.Get("Uuid");
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override IGetCategories ParserData(JsonElement data)
        {
            return new GetCategories
            (
                ParseGetCategoriesCategories(data, "categories")
            );

        }

        private global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.ICategoryDto1> ParseGetCategoriesCategories(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            int objLength = obj.GetArrayLength();
            var list = new global::CoolStore.WebUI.Host.ICategoryDto1[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new CategoryDto1
                (
                    DeserializeUuid(element, "id"),
                    DeserializeString(element, "name")
                );

            }

            return list;
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
