using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_leave_management_system.Migrations
{
    /// <inheritdoc />
    public partial class add_constraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // LeaveQuota: TotalUsed cannot exceed TotalAllocated
            migrationBuilder.Sql(@"
                ALTER TABLE LeaveQuotas 
                ADD CONSTRAINT CK_LeaveQuotas_TotalUsed 
                CHECK (TotalUsed >= 0 AND TotalUsed <= TotalAllocated)
            ");

            // LeaveQuota: TotalAllocated must be valid range
            migrationBuilder.Sql(@"
                ALTER TABLE LeaveQuotas 
                ADD CONSTRAINT CK_LeaveQuotas_TotalAllocated 
                CHECK (TotalAllocated >= 0 AND TotalAllocated <= 365)
            ");

            // LeaveApplication: ToDate >= FromDate
            migrationBuilder.Sql(@"
                ALTER TABLE LeaveApplications 
                ADD CONSTRAINT CK_LeaveApplications_DateRange 
                CHECK (ToDate >= FromDate)
            ");

            // LeaveApplication: TotalDays must be positive
            migrationBuilder.Sql(@"
                ALTER TABLE LeaveApplications 
                ADD CONSTRAINT CK_LeaveApplications_TotalDays 
                CHECK (TotalDays > 0 AND TotalDays <= 365)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Detect database provider
            var isMySql = migrationBuilder.ActiveProvider?.Contains("MySql", StringComparison.OrdinalIgnoreCase) ?? false;

            if (isMySql)
            {
                // MySQL syntax: DROP CHECK
                migrationBuilder.Sql("ALTER TABLE LeaveQuotas DROP CHECK CK_LeaveQuotas_TotalUsed");
                migrationBuilder.Sql("ALTER TABLE LeaveQuotas DROP CHECK CK_LeaveQuotas_TotalAllocated");
                migrationBuilder.Sql("ALTER TABLE LeaveApplications DROP CHECK CK_LeaveApplications_DateRange");
                migrationBuilder.Sql("ALTER TABLE LeaveApplications DROP CHECK CK_LeaveApplications_TotalDays");
            }
            else
            {
                // SQL Server syntax: DROP CONSTRAINT
                migrationBuilder.Sql("ALTER TABLE LeaveQuotas DROP CONSTRAINT CK_LeaveQuotas_TotalUsed");
                migrationBuilder.Sql("ALTER TABLE LeaveQuotas DROP CONSTRAINT CK_LeaveQuotas_TotalAllocated");
                migrationBuilder.Sql("ALTER TABLE LeaveApplications DROP CONSTRAINT CK_LeaveApplications_DateRange");
                migrationBuilder.Sql("ALTER TABLE LeaveApplications DROP CONSTRAINT CK_LeaveApplications_TotalDays");
            }
        }
    }
}
