using System;

namespace LeavePortal.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
