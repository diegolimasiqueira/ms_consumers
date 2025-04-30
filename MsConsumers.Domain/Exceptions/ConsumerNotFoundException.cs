using System;

namespace MsConsumers.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a consumer is not found
    /// </summary>
    public class ConsumerNotFoundException : Exception
    {
        /// <summary>
        /// Consumer ID that was not found
        /// </summary>
        public Guid ConsumerId { get; }

        /// <summary>
        /// Initializes a new instance of the ConsumerNotFoundException
        /// </summary>
        /// <param name="consumerId">The ID of the consumer that was not found</param>
        public ConsumerNotFoundException(Guid consumerId)
            : base($"Consumer with ID {consumerId} was not found")
        {
            ConsumerId = consumerId;
        }
    }
} 