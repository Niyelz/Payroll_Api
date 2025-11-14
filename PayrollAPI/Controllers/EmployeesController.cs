using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollApi.Data;
using PayrollApi.Models;

namespace PayrollApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;


        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee emp)
        {
            emp.EmployeeNumber = GenerateEmployeeNumber(emp); // auto-generate
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = emp.Id }, emp);
        }



        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee emp)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null) return NotFound();

            existing.FirstName = emp.FirstName;
            existing.LastName = emp.LastName;
            existing.MiddleName = emp.MiddleName;
            existing.DateOfBirth = emp.DateOfBirth;
            existing.DailyRate = emp.DailyRate;
            existing.WorkingDays = emp.WorkingDays;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/employees/compute
        [HttpPost("compute")]
        public ActionResult<decimal> ComputeTakeHomePay([FromBody] ComputeRequest request)
        {
            var emp = _context.Employees.FirstOrDefault(e => e.Id == request.EmployeeId);
            if (emp == null) return NotFound("Employee not found");

            decimal totalPay = 0;
            var daysWorked = GetWorkingDays(emp.WorkingDays, request.StartDate, request.EndDate);

            foreach (var day in daysWorked)
            {
                totalPay += emp.DailyRate * 2; // Twice the daily rate
            }

            // Birthday bonus
            for (var dt = request.StartDate; dt <= request.EndDate; dt = dt.AddDays(1))
            {
                if (dt.Day == emp.DateOfBirth.Day && dt.Month == emp.DateOfBirth.Month)
                {
                    totalPay += emp.DailyRate;
                }
            }

            return Ok(totalPay);
        }

        // Helper Methods
        private string GenerateEmployeeNumber(Employee emp)
        {
            var first3 = (emp.LastName + emp.FirstName + (emp.MiddleName ?? "")).ToUpper().Substring(0, 3);
            var randomNum = new Random().Next(0, 99999).ToString("D5");
            var dob = emp.DateOfBirth.ToString("ddMMMyyyy").ToUpper();
            return $"{first3}-{randomNum}-{dob}";
        }

        private List<DateTime> GetWorkingDays(string workingDays, DateTime start, DateTime end)
        {
            List<DateTime> result = new();
            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                if (workingDays == "MWF" && (dt.DayOfWeek == DayOfWeek.Monday || dt.DayOfWeek == DayOfWeek.Wednesday || dt.DayOfWeek == DayOfWeek.Friday))
                    result.Add(dt);
                else if (workingDays == "TTHS" && (dt.DayOfWeek == DayOfWeek.Tuesday || dt.DayOfWeek == DayOfWeek.Thursday || dt.DayOfWeek == DayOfWeek.Saturday))
                    result.Add(dt);
            }
            return result;
        }
    }

    public class ComputeRequest
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
