using System;

namespace MsConsumers.Domain.Exceptions
{
    public class UniqueConstraintViolationException : Exception
    {
        public string FieldName { get; }
        public string FieldValue { get; }

        public UniqueConstraintViolationException(string fieldName, string fieldValue)
            : base($"The value '{fieldValue}' is already in use for the field '{fieldName}'")
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Field name cannot be null or empty", nameof(fieldName));

            if (string.IsNullOrWhiteSpace(fieldValue))
                throw new ArgumentException("Field value cannot be null or empty", nameof(fieldValue));

            FieldName = fieldName;
            FieldValue = fieldValue;
        }
    }
} 