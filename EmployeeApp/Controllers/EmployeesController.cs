#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApp.Data;
using EmployeeApp.Models;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeesController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmpolyees()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        // GET: api/Employees/5
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetEmployee")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x =>x.EmpId==id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employee employee)
        {
            var existingEmployeee = await _context.Employees.FirstOrDefaultAsync(x => x.EmpId == id);
            if(existingEmployeee != null)
            {
                existingEmployeee.EmployeeName = employee.EmployeeName;
                existingEmployeee.EmployeeMail = employee.EmployeeMail;
                existingEmployeee.EmployeePhone = employee.EmployeePhone;
                existingEmployeee.EmployeePassword = employee.EmployeePassword;
                await _context.SaveChangesAsync();
                return Ok(existingEmployeee);
            }

            return NotFound("Employee not Found");
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.EmpId = Guid.NewGuid();
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), employee.EmpId, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var existingEmployeee = await _context.Employees.FirstOrDefaultAsync(x => x.EmpId == id);
            if (existingEmployeee != null)
            {
                _context.Employees.Remove(existingEmployeee);
                await _context.SaveChangesAsync();
                return Ok(existingEmployeee);

            }


            return NotFound("Employee not found!");
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
