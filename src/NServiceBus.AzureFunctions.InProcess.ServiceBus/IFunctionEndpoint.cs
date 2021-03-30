namespace NServiceBus
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.ServiceBus;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// An NServiceBus endpoint hosted in Azure Function which does not receive messages automatically but only handles
    /// messages explicitly passed to it by the caller.
    /// </summary>
    public interface IFunctionEndpoint
    {
        /// <summary>
        /// Processes a message received from an AzureServiceBus trigger using the NServiceBus message pipeline.
        /// </summary>
        Task Process(Message message, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Sends the provided message.
        /// </summary>
        Task Send(object message, SendOptions options, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Sends the provided message.
        /// </summary>
        Task Send(object message, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Instantiates a message of type T and sends it.
        /// </summary>
        Task Send<T>(Action<T> messageConstructor, SendOptions options, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Instantiates a message of type T and sends it.
        /// </summary>
        Task Send<T>(Action<T> messageConstructor, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Publish the message to subscribers.
        /// </summary>
        Task Publish(object message, PublishOptions options, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Instantiates a message of type T and publishes it.
        /// </summary>
        Task Publish<T>(Action<T> messageConstructor, PublishOptions options, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Instantiates a message of type T and publishes it.
        /// </summary>
        Task Publish(object message, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Instantiates a message of type T and publishes it.
        /// </summary>
        Task Publish<T>(Action<T> messageConstructor, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Subscribes to receive published messages of the specified type.
        /// This method is only necessary if you turned off auto-subscribe.
        /// </summary>
        Task Subscribe(Type eventType, SubscribeOptions options, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Subscribes to receive published messages of the specified type.
        /// This method is only necessary if you turned off auto-subscribe.
        /// </summary>
        Task Subscribe(Type eventType, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Unsubscribes to receive published messages of the specified type.
        /// </summary>
        Task Unsubscribe(Type eventType, UnsubscribeOptions options, FunctionContext functionContext, ILogger functionsLogger = null);

        /// <summary>
        /// Unsubscribes to receive published messages of the specified type.
        /// </summary>
        Task Unsubscribe(Type eventType, FunctionContext functionContext, ILogger functionsLogger = null);
    }
}