using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Infrastructure.Repository;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IRepositoryWrapper
            _context;

        public EmployeesController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            List<Employee>? employees = await _context.Employees.GetAllAsync(e => e.Department) as List<Employee>;
            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id, e => e.Department);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            List<Department>? departments = await _context.Departments.GetAllAsync() as List<Department>;
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeCode,FirstName,LastName,DepartmentId,DateOfJoining,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _context.Employees.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            List<Department>? departments = await _context.Departments.GetAllAsync() as List<Department>;
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            List<Department>? departments = await _context.Departments.GetAllAsync() as List<Department>;
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeCode,FirstName,LastName,DepartmentId,DateOfJoining,IsActive")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Employees.UpdateAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            List<Department>? departments = await _context.Departments.GetAllAsync() as List<Department>;
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id, e => e.Department);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id);
            if (employee != null)
            {
                await _context.Employees.DeleteAsync(employee);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await _context.Employees.ExistsAsync(e => e.EmployeeId == id);
        }
    }
}
