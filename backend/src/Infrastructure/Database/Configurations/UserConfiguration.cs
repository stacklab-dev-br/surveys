﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackLab.Survey.Domain.Common.Enums;
using StackLab.Survey.Domain.Entities;

namespace StackLab.Survey.Infrastructure.Database;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(x => x.Id)
            .UseMySqlIdentityColumn();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder
            .Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Status)
            .HasConversion(new EnumToStringConverter<Status>())
            .HasMaxLength(16);

        builder.HasMany(x => x.VerificationTokens).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
