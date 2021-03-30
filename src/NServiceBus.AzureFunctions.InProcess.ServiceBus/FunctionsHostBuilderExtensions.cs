namespace NServiceBus
{
    using System;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Provides extension methods to configure a <see cref="FunctionEndpoint"/> using <see cref="IFunctionsWorkerApplicationBuilder"/>.
    /// </summary>
    public static class FunctionsHostBuilderExtensions
    {
        /// <summary>
        /// Configures an NServiceBus endpoint that can be injected into a function trigger as a <see cref="FunctionEndpoint"/> via dependency injection.
        /// </summary>
        public static void UseNServiceBus(
            this IFunctionsWorkerApplicationBuilder functionsHostBuilder,
            Func<ServiceBusTriggeredEndpointConfiguration> configurationFactory)
        {
            var serviceBusTriggeredEndpointConfiguration = configurationFactory();

            var endpointFactory = Configure(serviceBusTriggeredEndpointConfiguration, functionsHostBuilder.Services);

            // for backward compatibility
            functionsHostBuilder.Services.AddSingleton(endpointFactory);
            functionsHostBuilder.Services.AddSingleton<IFunctionEndpoint>(sp => sp.GetRequiredService<FunctionEndpoint>());
        }

        internal static Func<IServiceProvider, FunctionEndpoint> Configure(
            ServiceBusTriggeredEndpointConfiguration configuration,
            IServiceCollection serviceCollection)
        {
            var startableEndpoint = EndpointWithExternallyManagedServiceProvider.Create(
                    configuration.EndpointConfiguration,
                    serviceCollection);

            return serviceProvider => new FunctionEndpoint(startableEndpoint, configuration, serviceProvider);
        }
    }
}