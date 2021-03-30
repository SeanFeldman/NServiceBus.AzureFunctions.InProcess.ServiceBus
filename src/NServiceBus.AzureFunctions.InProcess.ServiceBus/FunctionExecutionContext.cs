namespace NServiceBus
{
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Contains specific context information of the current function invocation.
    /// </summary>
    public class FunctionExecutionContext
    {
        /// <summary>
        /// Creates a new <see cref="FunctionExecutionContext"/>.
        /// </summary>
        public FunctionExecutionContext(FunctionContext functionContext, ILogger logger)
        {
            Logger = logger;
            FunctionContext = functionContext;
        }

        /// <summary>
        /// The <see cref="FunctionContext"/> associated with the current function invocation.
        /// </summary>
        public FunctionContext FunctionContext { get; }

        /// <summary>
        /// The <see cref="ILogger"/> associated with the current function invocation.
        /// </summary>
        public ILogger Logger { get; }
    }
}