using Ambev.DeveloperEvaluation.Common.Primitives.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public interface IDefaultContext
{
}

public class DefaultContext(
    DbContextOptions<DefaultContext> options
    //,IPublisher publisher
    ) : DbContext(options), IDefaultContext
{
    //private readonly IPublisher _publisher = publisher;
    public DbSet<User> Users => Set<User>();

    public override async Task<int> SaveChangesAsync(
          CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        // - domain events are part of the same transaction
        // - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        // - domain events are a separate transaction
        // - eventual consistency
        // - handlers can fail

        var result = await base.SaveChangesAsync(cancellationToken);

        var hasEvents = await PublishDomainEventsAsync();

        if (hasEvents)
        {
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("identity");

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //builder.ApplyConfiguration(new UserConfiguration());
    }

    private async Task<bool> PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetAndClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            // await _publisher.Publish(domainEvent);
        }

        return domainEvents.Count != 0;
    }
}

//public class DefaultContext : DbContext
//{
//    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
//    {
//    }

// public DbSet<User> Users { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
//        base.OnModelCreating(modelBuilder);
//    }
//}

//public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
//{
//    public DefaultContext CreateDbContext(string[] args)
//    {
//        IConfigurationRoot configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json")
//            .Build();

// var builder = new DbContextOptionsBuilder<DefaultContext>(); var connectionString = configuration.GetConnectionString("DefaultConnection");

// builder.UseNpgsql( connectionString, b =>
// b.MigrationsAssembly("Ambev.DeveloperEvaluation.WebApi") );

//        return new DefaultContext(builder.Options);
//    }
//}
