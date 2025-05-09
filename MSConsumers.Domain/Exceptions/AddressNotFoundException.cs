using System;

namespace MSConsumers.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when an address is not found
    /// </summary>
    public class AddressNotFoundException : Exception
    {
        /// <summary>
        /// Address ID that was not found
        /// </summary>
        public Guid AddressId { get; }

        /// <summary>
        /// Initializes a new instance of the AddressNotFoundException
        /// </summary>
        /// <param name="addressId">The ID of the address that was not found</param>
        public AddressNotFoundException(Guid addressId)
            : base($"Endereço com ID {addressId} não encontrado")
        {
            AddressId = addressId;
        }
    }
} 