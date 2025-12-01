using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Models;
using simple_leave_management_system.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace simple_leave_management_system.Infrastructure
{
    public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveQuota> LeaveQuotas { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        //    // ===== UNIQUE CONSTRAINTS =====

        //    // Department - DepartmentName Unique
        //    modelBuilder.Entity<Department>()
        //        .HasIndex(d => d.DepartmentName)
        //        .IsUnique()
        //        .HasDatabaseName("UQ_Departments_DepartmentName");

        //    // Employee - EmployeeCode Unique
        //    modelBuilder.Entity<Employee>()
        //        .HasIndex(e => e.EmployeeCode)
        //        .IsUnique()
        //        .HasDatabaseName("UQ_Employees_EmployeeCode");

        //    // LeaveType - LeaveTypeCode Unique
        //    modelBuilder.Entity<LeaveType>()
        //        .HasIndex(lt => lt.LeaveTypeCode)
        //        .IsUnique()
        //        .HasDatabaseName("UQ_LeaveTypes_LeaveTypeCode");

        //    // LeaveQuota - Composite Unique (EmployeeId, LeaveTypeId, LeaveYear)
        //    modelBuilder.Entity<LeaveQuota>()
        //        .HasIndex(lq => new { lq.EmployeeId, lq.LeaveTypeId, lq.LeaveYear })
        //        .IsUnique()
        //        .HasDatabaseName("UQ_Employee_LeaveType_Year");

        // ===== RELATIONSHIPS =====

        // Department -> Employees (1:Many)
        modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Employees_Departments");

        // Employee -> LeaveQuotas (1:Many)
        modelBuilder.Entity<LeaveQuota>()
                .HasOne(lq => lq.Employee)
                .WithMany(e => e.LeaveQuotas)
                .HasForeignKey(lq => lq.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Quota_Employee");

        // LeaveType -> LeaveQuotas (1:Many)
        modelBuilder.Entity<LeaveQuota>()
                .HasOne(lq => lq.LeaveType)
                .WithMany(lt => lt.LeaveQuotas)
                .HasForeignKey(lq => lq.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Quota_LeaveType");

        // Employee -> LeaveApplications (1:Many)
        modelBuilder.Entity<LeaveApplication>()
                .HasOne(la => la.Employee)
                .WithMany(e => e.LeaveApplications)
                .HasForeignKey(la => la.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_LeaveApp_Employee");

        // LeaveType -> LeaveApplications (1:Many)
        modelBuilder.Entity<LeaveApplication>()
                .HasOne(la => la.LeaveType)
                .WithMany(lt => lt.LeaveApplications)
                .HasForeignKey(la => la.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_LeaveApp_LeaveType");

        // Self-referencing for Approver (Employee approves Employee's leave)
        modelBuilder.Entity<LeaveApplication>()
                .HasOne(la => la.Approver)
                .WithMany()
                .HasForeignKey(la => la.ApprovedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_LeaveApp_Approver");

            // ===== DEFAULT VALUES =====

            modelBuilder.Entity<Department>()
                .Property(d => d.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Employee>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<LeaveType>()
                .Property(lt => lt.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<LeaveType>()
                .Property(lt => lt.IsPaidLeave)
                .HasDefaultValue(true);

            modelBuilder.Entity<LeaveQuota>()
                .Property(lq => lq.TotalUsed)
                .HasDefaultValue(0);

            modelBuilder.Entity<LeaveApplication>()
                .Property(la => la.Status)
                .HasDefaultValue("Pending");
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            ValidateEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            ValidateEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Base &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Base)entry.Entity;

                if (entry.State == EntityState.Added) entity.CreatedAt = DateTime.Now;

                entity.UpdatedAt = DateTime.Now;
            }
        }

        private void ValidateEntities()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(entity, validationContext, validationResults, true))
                {
                    var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                    throw new ValidationException($"Validation failed: {errors}");
                }
            }
        }
    }
}
