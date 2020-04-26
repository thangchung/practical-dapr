// using Grpc.Core;
// using Grpc.Core.Interceptors;
//
// namespace N8T.Infrastructure.Grpc
// {
//     public class ClientLoggerInterceptor : Interceptor
//     {
//         public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
//             TRequest request,
//             ClientInterceptorContext<TRequest, TResponse> context,
//             AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
//         {
//             AddCallerMetadata(ref context);
//
//             return continuation(request, context);
//         }
//
//         private void AddCallerMetadata<TRequest, TResponse>(ref ClientInterceptorContext<TRequest, TResponse> context)
//             where TRequest : class
//             where TResponse : class
//         {
//             // var headers = context.Options.Headers;
//             // // Call doesn't have a headers collection to add to.
//             // // Need to create a new context with headers for the call.
//             // if (headers == null)
//             // {
//             //     headers = new Metadata();
//             //     var options = context.Options.WithHeaders(headers);
//             //     context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
//             // }
//         }
//     }
// }
