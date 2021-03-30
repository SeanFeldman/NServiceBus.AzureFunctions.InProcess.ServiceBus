namespace NServiceBus.AzureFunctions.InProcess.ServiceBus
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Azure.Functions.Worker;

    static class MessageExtensions
    {
        public static Dictionary<string, string> GetHeaders(this FunctionContext functionContext)
        {
            var headers = new Dictionary<string, string>();

            if (functionContext.BindingContext.BindingData.TryGetValue("UserProperties", out var customProperties) && customProperties != null)
            {
                var customHeaders = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(customProperties.ToString() ?? throw new InvalidOperationException());

                if (customHeaders != null)
                {
                    foreach ((string key, string value) in customHeaders)
                    {
                        headers[key] = value;
                    }
                }
            }

            headers.Remove("NServiceBus.Transport.Encoding");

            if (functionContext.BindingContext.BindingData.ContainsKey("ReplyTo"))
            {
                headers[Headers.ReplyToAddress] = functionContext.BindingContext.BindingData["ReplyTo"]?.ToString();
            }

            if (functionContext.BindingContext.BindingData.ContainsKey("CorrelationId"))
            {
                headers[Headers.ReplyToAddress] = functionContext.BindingContext.BindingData["CorrelationId"]?.ToString();
            }

            return headers;
        }

        public static string GetMessageId(this FunctionContext functionContext)
        {
            if (functionContext.BindingContext.BindingData.TryGetValue("MessageId", out var messageId) == false || messageId == null)
            {
                // assume native functionContext w/o functionContext ID
                return Guid.NewGuid().ToString("N");
            }

            return messageId.ToString();
        }

        public static int GetDeliveryCount(this FunctionContext functionContext)
        {
            int.TryParse(functionContext.BindingContext.BindingData["DeliveryCount"] as string, out var deliveryCount);

            return deliveryCount;
        }
    }
}