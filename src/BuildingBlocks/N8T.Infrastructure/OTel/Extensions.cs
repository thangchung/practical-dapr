using System;
using N8T.Infrastructure.OTel.MediatR;
using OpenTelemetry.Trace;

namespace N8T.Infrastructure.OTel
{
    public static class Extensions
    {
        public static TracerProviderBuilder AddMediatRInstrumentation(
            this TracerProviderBuilder builder)
         {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddInstrumentation((activitySource) => new OTelMediatRInstrumentation(activitySource));
            builder.AddActivitySource(OTelMediatROptions.OTelMediatRName);
            return builder;
        }
    }
}
