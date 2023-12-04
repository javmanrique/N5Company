using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using N5Company.Domain.Models;

namespace N5Company.DataAccess;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PermissionTypes> PermissionTypes { get; set; }

    public virtual DbSet<Permissions> Permissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=sql5106.site4now.net;user=db_a42e7c_n5company_admin;password=JwG3HMNwXR7jdBV;database=db_a42e7c_n5company");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermissionTypes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC074BB88532");
        });

        modelBuilder.Entity<Permissions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC079EE6168C");

            entity.Property(e => e.PermissionDate).HasColumnType("date");

            entity.HasOne(d => d.PermissionTypeNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permissio__Permi__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
