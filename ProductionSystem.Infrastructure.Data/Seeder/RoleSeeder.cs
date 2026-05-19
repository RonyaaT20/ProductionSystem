using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductionSystem.Domain.Entities;
using System;

namespace ProductionSystem.Infrastructure.Data.Seeder
{
    public class RoleSeeder : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role
            {
                Id = 1,
                Title = "SuperAdmin",
                CreatedAt = new DateTime(2026, 10, 14, 7, 8, 0, 0, DateTimeKind.Local).AddTicks(2972)
            });
        }
    }
}

