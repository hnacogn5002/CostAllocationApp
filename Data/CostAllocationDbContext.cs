using Microsoft.EntityFrameworkCore;
using CostAllocationApp.Models;

public class CostAllocationDbContext : DbContext
{
    public CostAllocationDbContext(
        DbContextOptions<CostAllocationDbContext> options)
        : base(options) { }

    public DbSet<Department> Departments { get; set; }
    public DbSet<OverheadCost> OverheadCosts { get; set; }
    public DbSet<CostAllocation> CostAllocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình quan hệ nếu cần
        modelBuilder.Entity<CostAllocation>()
            .HasOne(c => c.OverheadCost)
            .WithMany(o => o.CostAllocations)
            .HasForeignKey(c => c.OverheadCostId);
        modelBuilder.Entity<CostAllocation>()
                   .HasOne(c => c.Department)
                   .WithMany(d => d.CostAllocations)
                   .HasForeignKey(c => c.DepartmentId);
    }
}