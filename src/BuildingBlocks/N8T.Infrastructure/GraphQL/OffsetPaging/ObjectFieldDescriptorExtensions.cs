using System.Threading.Tasks;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace N8T.Infrastructure.GraphQL.OffsetPaging
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UseOffsetPaging<TSchemaType, TValueType>(
            this IObjectFieldDescriptor descriptor)
            where TSchemaType : class, IOutputType
            where TValueType : new()
        {
            static FieldDelegate Placeholder(FieldDelegate next) => context => Task.CompletedTask;
            var middlewareDefinition = typeof(QueryableConnectionMiddleware<>);

            descriptor
                .AddPagingArguments()
                .Type<OffsetPagingType<TSchemaType, TValueType>>()
                .Use(Placeholder)
                .Extend()
                .OnBeforeCompletion((context, definition) =>
                {
                    var reference = new ClrTypeReference(typeof(TSchemaType), TypeContext.Output);
                    var type = context.GetType<IOutputType>(reference);

                    if (!(type.NamedType() is IHasClrType hasClrType)) return;
                    var middlewareType = middlewareDefinition.MakeGenericType(hasClrType.ClrType);
                    var middleware = FieldClassMiddlewareFactory.Create(middlewareType);
                    var index = definition.MiddlewareComponents.IndexOf(Placeholder);
                    definition.MiddlewareComponents[index] = middleware;
                })
                .DependsOn<TSchemaType>();

            return descriptor;
        }

        private static IObjectFieldDescriptor AddPagingArguments(this IObjectFieldDescriptor descriptor)
        {
            return descriptor
                .Argument("page", a => a.Type<IntType>())
                .Argument("pageSize", a => a.Type<IntType>());
        }
    }
}