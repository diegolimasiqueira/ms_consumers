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
            FieldName = fieldName;
            FieldValue = fieldValue;
        }
    }
} 