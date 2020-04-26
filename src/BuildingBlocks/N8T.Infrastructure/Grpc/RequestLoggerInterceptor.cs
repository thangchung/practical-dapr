// using System;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Grpc.Core;
// using Grpc.Core.Interceptors;
// using Serilog;
// using Serilog.Context;
//
// namespace N8T.Infrastructure.Grpc
// {
//     /// <summary>
//     /// Ref at https://gsferreira.com/archive/2019/04/logging-grpc-requests-using-serilog/
//     /// </summary>
//     public class RequestLoggerInterceptor : Interceptor
//     {
//         private const string MessageTemplate = "{RequestMethod} responded {StatusCode} in {Elapsed:0.0000} ms";
//
//         public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
//             ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
//         {
//             var requestId = context
//                 .RequestHeaders
//                 .FirstOrDefault(h => h.Key.Equals("traceparent", StringComparison.OrdinalIgnoreCase))?.Value;
//
//             using (LogContext.PushProperty("traceparent", requestId))
//             {
//                 var sw = Stopwatch.StartNew();
//
//                 var response = await base.UnaryServerHandler(request, context, continuation);
//
//                 sw.Stop();
//
//                 Log.Logger.Information(MessageTemplate,
//                     context.Method,
//                     context.Status.StatusCode,
//                     sw.Elapsed.TotalMilliseconds);
//
//                 return response;
//             }
//         }
//     }
// }
