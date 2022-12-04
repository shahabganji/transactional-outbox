// See https://aka.ms/new-console-template for more information

using Demo.TransactionalOutbox.Domain.Abstractions;
using Demo.TransactionalOutbox.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

Console.WriteLine("This is the database migration designer project!");


public sealed  class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        optionsBuilder.UseNpgsql("Host=localhost; Database=orders; Username=demo; Password=strong;");

        return new OrderContext(optionsBuilder.Options, new NullEventEmitter());
    }
}

public sealed class NullEventEmitter : IEventEmitter
{
    public Task Emit(IEnumerable<IDomainEvent> domainEvents) => Task.CompletedTask;
} 
