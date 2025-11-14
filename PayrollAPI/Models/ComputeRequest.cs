using System;

namespace PayrollApi.Models
{
    public class ComputeRequest
    {
        public string EmployeeNumber { get; set; } = null!; // or int Id if you prefer numeric ID
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
