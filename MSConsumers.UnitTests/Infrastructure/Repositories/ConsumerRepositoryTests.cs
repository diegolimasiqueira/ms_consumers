using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using MSConsumers.Domain.Entities;
using MSConsumers.Infrastructure.Data;
using MSConsumers.Infrastructure.Repositories;
using Xunit;

namespace MSConsumers.UnitTests.Infrastructure.Repositories;

public class ConsumerRepositoryTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly Mock<DbSet<ConsumerEntity>> _dbSetMock;
    private readonly ConsumerRepository _repository;

    public ConsumerRepositoryTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _dbSetMock = new Mock<DbSet<ConsumerEntity>>();
        
        // Configuração do DbSet mock
        var consumers = new List<ConsumerEntity>().AsQueryable();
        
        // Setup IQueryable
        _dbSetMock.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<ConsumerEntity>(consumers.Provider));
        
        _dbSetMock.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.Expression)
            .Returns(consumers.Expression);
        
        _dbSetMock.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.ElementType)
            .Returns(consumers.ElementType);
        
        _dbSetMock.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.GetEnumerator())
            .Returns(consumers.GetEnumerator());

        // Setup IAsyncEnumerable
        _dbSetMock.As<IAsyncEnumerable<ConsumerEntity>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<ConsumerEntity>(consumers.GetEnumerator()));
        
        _contextMock.Setup(x => x.Consumers).Returns(_dbSetMock.Object);
        _repository = new ConsumerRepository(_contextMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnConsumer()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var consumer = new ConsumerEntity(
            "Test Consumer",
            "12345678900",
            "https://example.com/photo.jpg",
            "11999999999",
            "test@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        _dbSetMock
            .Setup(x => x.FindAsync(consumerId))
            .ReturnsAsync(consumer);

        // Act
        var result = await _repository.GetByIdAsync(consumerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(consumer.Id, result.Id);
        Assert.Equal(consumer.Name, result.Name);
        Assert.Equal(consumer.DocumentId, result.DocumentId);
        Assert.Equal(consumer.PhotoUrl, result.PhotoUrl);
        Assert.Equal(consumer.PhoneNumber, result.PhoneNumber);
        Assert.Equal(consumer.Email, result.Email);
        Assert.Equal(consumer.CurrencyId, result.CurrencyId);
        Assert.Equal(consumer.PhoneCountryCodeId, result.PhoneCountryCodeId);
        Assert.Equal(consumer.PreferredLanguageId, result.PreferredLanguageId);
        Assert.Equal(consumer.TimezoneId, result.TimezoneId);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var consumerId = Guid.NewGuid();

        _dbSetMock
            .Setup(x => x.FindAsync(consumerId))
            .ReturnsAsync((ConsumerEntity?)null);

        // Act
        var result = await _repository.GetByIdAsync(consumerId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllConsumers()
    {
        // Arrange
        var consumers = new List<ConsumerEntity>
        {
            new ConsumerEntity(
                "Test Consumer 1",
                "12345678901",
                "https://example.com/photo1.jpg",
                "11999999991",
                "test1@test.com",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()),
            new ConsumerEntity(
                "Test Consumer 2",
                "12345678902",
                "https://example.com/photo2.jpg",
                "11999999992",
                "test2@test.com",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid())
        };

        var mockDbSet = new Mock<DbSet<ConsumerEntity>>();
        var queryableConsumers = consumers.AsQueryable();

        mockDbSet.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<ConsumerEntity>(queryableConsumers.Provider));

        mockDbSet.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.Expression)
            .Returns(queryableConsumers.Expression);

        mockDbSet.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.ElementType)
            .Returns(queryableConsumers.ElementType);

        mockDbSet.As<IQueryable<ConsumerEntity>>()
            .Setup(m => m.GetEnumerator())
            .Returns(queryableConsumers.GetEnumerator());

        mockDbSet.As<IAsyncEnumerable<ConsumerEntity>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<ConsumerEntity>(queryableConsumers.GetEnumerator()));

        _contextMock.Setup(x => x.Consumers).Returns(mockDbSet.Object);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Equal(consumers[0].Id, resultList[0].Id);
        Assert.Equal(consumers[1].Id, resultList[1].Id);
    }

    [Fact]
    public async Task AddAsync_ShouldAddConsumerAndSaveChanges()
    {
        // Arrange
        var consumer = new ConsumerEntity(
            "Test Consumer",
            "12345678900",
            "https://example.com/photo.jpg",
            "11999999999",
            "test@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        _dbSetMock
            .Setup(x => x.AddAsync(It.IsAny<ConsumerEntity>(), It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult((EntityEntry<ConsumerEntity>)null));

        _contextMock.Setup(x => x.Consumers).Returns(_dbSetMock.Object);
        _contextMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _repository.AddAsync(consumer);

        // Assert
        _dbSetMock.Verify(x => x.AddAsync(consumer, It.IsAny<CancellationToken>()), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateConsumerAndSaveChanges()
    {
        // Arrange
        var consumer = new ConsumerEntity(
            "Test Consumer",
            "12345678900",
            "https://example.com/photo.jpg",
            "11999999999",
            "test@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        _dbSetMock
            .Setup(x => x.Update(It.IsAny<ConsumerEntity>()));

        _contextMock.Setup(x => x.Consumers).Returns(_dbSetMock.Object);
        _contextMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _repository.UpdateAsync(consumer);

        // Assert
        _dbSetMock.Verify(x => x.Update(consumer), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingConsumer_ShouldDeleteConsumerAndSaveChanges()
    {
        // Arrange
        var consumerId = Guid.NewGuid();
        var consumer = new ConsumerEntity(
            "Test Consumer",
            "12345678900",
            "https://example.com/photo.jpg",
            "11999999999",
            "test@test.com",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        _dbSetMock
            .Setup(x => x.FindAsync(consumerId))
            .ReturnsAsync(consumer);

        _dbSetMock
            .Setup(x => x.Remove(It.IsAny<ConsumerEntity>()));

        _contextMock.Setup(x => x.Consumers).Returns(_dbSetMock.Object);
        _contextMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _repository.DeleteAsync(consumerId);

        // Assert
        _dbSetMock.Verify(x => x.Remove(consumer), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingConsumer_ShouldNotDeleteOrSaveChanges()
    {
        // Arrange
        var consumerId = Guid.NewGuid();

        _dbSetMock
            .Setup(x => x.FindAsync(consumerId))
            .ReturnsAsync((ConsumerEntity?)null);

        // Act
        await _repository.DeleteAsync(consumerId);

        // Assert
        _dbSetMock.Verify(x => x.Remove(It.IsAny<ConsumerEntity>()), Times.Never);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}

public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    public TestAsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return new TestAsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new TestAsyncEnumerable<TElement>(expression);
    }

    public object Execute(Expression expression)
    {
        return _inner.Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return _inner.Execute<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var expectedResultType = typeof(TResult).GetGenericArguments()[0];
        var executionResult = typeof(IQueryProvider)
            .GetMethod(
                name: nameof(IQueryProvider.Execute),
                genericParameterCount: 1,
                types: new[] { typeof(Expression) })
            .MakeGenericMethod(expectedResultType)
            .Invoke(this, new[] { expression });

        return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
            .MakeGenericMethod(expectedResultType)
            .Invoke(null, new[] { executionResult });
    }
}

public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public TestAsyncEnumerable(IEnumerable<T> enumerable)
        : base(enumerable)
    { }

    public TestAsyncEnumerable(Expression expression)
        : base(expression)
    { }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider
    {
        get { return new TestAsyncQueryProvider<T>(this); }
    }
}

public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    public void Dispose()
    {
        _inner.Dispose();
    }

    public T Current
    {
        get { return _inner.Current; }
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_inner.MoveNext());
    }

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        return new ValueTask();
    }
} 