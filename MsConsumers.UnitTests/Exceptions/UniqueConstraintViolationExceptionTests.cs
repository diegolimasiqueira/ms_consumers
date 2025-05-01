using System;
using MsConsumers.Domain.Exceptions;
using Xunit;

namespace MsConsumers.UnitTests.Exceptions;

public class UniqueConstraintViolationExceptionTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetProperties()
    {
        // Arrange
        var fieldName = "Email";
        var fieldValue = "test@test.com";
        var expectedMessage = $"The value '{fieldValue}' is already in use for the field '{fieldName}'";

        // Act
        var exception = new UniqueConstraintViolationException(fieldName, fieldValue);

        // Assert
        Assert.Equal(fieldName, exception.FieldName);
        Assert.Equal(fieldValue, exception.FieldValue);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null, "test@test.com")]
    [InlineData("", "test@test.com")]
    [InlineData(" ", "test@test.com")]
    [InlineData("Email", null)]
    [InlineData("Email", "")]
    [InlineData("Email", " ")]
    public void Constructor_WithInvalidParameters_ShouldThrowArgumentException(string fieldName, string fieldValue)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new UniqueConstraintViolationException(fieldName, fieldValue));
    }
} 