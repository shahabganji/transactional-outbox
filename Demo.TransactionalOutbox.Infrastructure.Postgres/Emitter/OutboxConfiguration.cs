using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.TransactionalOutbox.Infrastructure.Postgres.Emitter;

internal sealed class OutboxConfiguration : IEntityTypeConfiguration<Outbox>
{
    public void Configure(EntityTypeBuilder<Outbox> builder)
    {
        builder.ToTable("outbox");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever().IsRequired();

        builder.Property(p => p.IsPublished).HasDefaultValue(false);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.PublishedAt).IsRequired(false);
        
        builder.Property(p => p.Payload).IsRequired();
        builder.Property(p => p.Type).IsRequired();

        builder.HasIndex(p => new { p.IsPublished, p.CreatedAt });
    }
}
