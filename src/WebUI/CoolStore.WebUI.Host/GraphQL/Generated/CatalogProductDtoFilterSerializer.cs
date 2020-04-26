using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CatalogProductDtoFilterSerializer
        : IInputSerializer
    {
        private bool _needsInitialization = true;
        private IValueSerializer _catalogProductDtoFilterSerializer;
        private IValueSerializer _stringSerializer;
        private IValueSerializer _floatSerializer;

        public string Name { get; } = "CatalogProductDtoFilter";

        public ValueKind Kind { get; } = ValueKind.InputObject;

        public Type ClrType => typeof(CatalogProductDtoFilter);

        public Type SerializationType => typeof(IReadOnlyDictionary<string, object>);

        public void Initialize(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _catalogProductDtoFilterSerializer = serializerResolver.Get("CatalogProductDtoFilter");
            _stringSerializer = serializerResolver.Get("String");
            _floatSerializer = serializerResolver.Get("Float");
            _needsInitialization = false;
        }

        public object Serialize(object value)
        {
            if (_needsInitialization)
            {
                throw new InvalidOperationException(
                    $"The serializer for type `{Name}` has not been initialized.");
            }

            if (value is null)
            {
                return null;
            }

            var input = (CatalogProductDtoFilter)value;
            var map = new Dictionary<string, object>();

            if (input.AND.HasValue)
            {
                map.Add("AND", SerializeNullableListOfCatalogProductDtoFilter(input.AND.Value));
            }

            if (input.Name.HasValue)
            {
                map.Add("name", SerializeNullableString(input.Name.Value));
            }

            if (input.NameContains.HasValue)
            {
                map.Add("name_contains", SerializeNullableString(input.NameContains.Value));
            }

            if (input.OR.HasValue)
            {
                map.Add("OR", SerializeNullableListOfCatalogProductDtoFilter(input.OR.Value));
            }

            if (input.Price.HasValue)
            {
                map.Add("price", SerializeNullableFloat(input.Price.Value));
            }

            if (input.PriceGte.HasValue)
            {
                map.Add("price_gte", SerializeNullableFloat(input.PriceGte.Value));
            }

            if (input.PriceLte.HasValue)
            {
                map.Add("price_lte", SerializeNullableFloat(input.PriceLte.Value));
            }

            return map;
        }

        private object SerializeNullableCatalogProductDtoFilter(object value)
        {
            return _catalogProductDtoFilterSerializer.Serialize(value);
        }

        private object SerializeNullableListOfCatalogProductDtoFilter(object value)
        {
            if (value is null)
            {
                return null;
            }


            IList source = (IList)value;
            object[] result = new object[source.Count];
            for(int i = 0; i < source.Count; i++)
            {
                result[i] = SerializeNullableCatalogProductDtoFilter(source[i]);
            }
            return result;
        }
        private object SerializeNullableString(object value)
        {
            if (value is null)
            {
                return null;
            }


            return _stringSerializer.Serialize(value);
        }
        private object SerializeNullableFloat(object value)
        {
            if (value is null)
            {
                return null;
            }


            return _floatSerializer.Serialize(value);
        }

        public object Deserialize(object value)
        {
            throw new NotSupportedException(
                "Deserializing input values is not supported.");
        }
    }
}
