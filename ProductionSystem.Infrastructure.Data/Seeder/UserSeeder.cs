using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductionSystem.Domain.Entities;
using System;

public class UserSeeder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = 1,
            Username = "SuperAdmin",
            RoleId = 1,
            PasswordHash = "$2a$11$OqPI9OFlGlfmxDsQ35lB/e2UeAKz7wMzw0qN5cSq5pidrbk/4JaFG",
            FullName = "superAdmin",
            IsActive = true,
            Mobile = "",
            CreatedAt = new DateTime(2026, 10, 14, 7, 8, 0, 0, DateTimeKind.Local).AddTicks(2972),
        });
    }
}