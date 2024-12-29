using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Todo_API.Models;

public partial class TodoDbContext : DbContext
{
    public TodoDbContext()
    {
    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=dbcs");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
