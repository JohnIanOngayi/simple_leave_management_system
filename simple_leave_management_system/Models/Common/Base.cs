namespace simple_leave_management_system.Models.Common
{
    public abstract class Base
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
