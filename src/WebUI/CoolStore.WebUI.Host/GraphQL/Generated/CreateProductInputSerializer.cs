using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CreateProductInputSerializer
        : IInputSerializer
    {
        private bool _needsInitialization = true;
        private IValueSerializer _uuidSerializer;
        private IValueSerializer _stringSerializer;
        private IValueSerializer _floatSerializer;

        public string Name { get; } = "CreateProductInput";

        public ValueKind Kind { get; } = ValueKind.InputObject;

        public Type ClrType => typeof(CreateProductInput);

        public Type SerializationType => typeof(IReadOnlyDictionary<string, object>);

        public void Initialize(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _uuidSerializer = serializerResolver.Get("Uuid");
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

            var input = (CreateProductInput)value;
            var map = new Dictionary<string, object>();

            if (input.CategoryId.HasValue)
            {
                map.Add("categoryId", SerializeNullableUuid(input.CategoryId.Value));
            }

            if (input.Description.HasValue)
            {
                map.Add("description", SerializeNullableString(input.Description.Value));
            }

            if (input.ImageUrl.HasValue)
            {
                map.Add("imageUrl", SerializeNullableString(input.ImageUrl.Value));
            }

            if (input.InventoryId.HasValue)
            {
                map.Add("inventoryId", SerializeNullableUuid(input.InventoryId.Value));
            }

            if (input.Name.HasValue)
            {
                map.Add("name", SerializeNullableString(input.Name.Value));
            }

            if (input.Price.HasValue)
            {
                map.Add("price", SerializeNullableFloat(input.Price.Value));
            }

            return map;
        }

        private object SerializeNullableUuid(object value)
        {
            return _uuidSerializer.Serialize(value);
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
            return _floatSerializer.Serialize(value);
        }

        public object Deserialize(object value)
        {
            throw new NotSupportedException(
                "Deserializing input values is not supported.");
        }
    }
}
