using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres.Configurations;

internal sealed class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();
        builder.Property(x => x.ProductId).IsRequired().ValueGeneratedNever();

        builder.Property(x => x.Quantity).IsRequired();
    }
}
