using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Boilerplate.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IContext
{
    public DbSet<Hero> Heroes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HeroConfiguration).Assembly);
    }
}