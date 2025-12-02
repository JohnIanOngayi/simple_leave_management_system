using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Infrastructure.Repository;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Controllers
{
    public class LeaveQuotasController : Controller
    {
        private readonly IRepositoryWrapper _context;

        public LeaveQuotasController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: LeaveQuotas
        public async Task<IActionResult> Index()
        {
            List<LeaveQuota>? leaveQuotas = await _context.LeaveQuotas.GetAllAsync(lq => lq.Employee, lq => lq.LeaveType) as List<LeaveQuota>;
            return View(leaveQuotas);
        }

        // GET: LeaveQuotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveQuota? leaveQuota = await _context.LeaveQuotas.FindOneAsync(lq => lq.LeaveQuotaId == id, lq => lq.Employee, lq => lq.LeaveType);
            if (leaveQuota == null)
            {
                return NotFound();
            }

            return View(leaveQuota);
        }

        // GET: LeaveQuotas/Create
        public async Task<IActionResult> Create()
        {
            List<Employee>? employees = await _context.Employees.GetAllAsync() as List<Employee>;
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;

            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText");
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName");

            return View();
        }

        // POST: LeaveQuotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveQuotaId,EmployeeId,LeaveTypeId,LeaveYear,TotalAllocated,TotalUsed")] LeaveQuota leaveQuota)
        {
            if (ModelState.IsValid)
            {
                await _context.LeaveQuotas.CreateAsync(leaveQuota);
                return RedirectToAction(nameof(Index));
            }

            List<Employee>? employees = await _context.Employees.GetAllAsync() as List<Employee>;
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;

            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveQuota.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName", leaveQuota.LeaveTypeId);
            return View(leaveQuota);
        }

        // GET: LeaveQuotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveQuota? leaveQuota = await _context.LeaveQuotas.FindOneAsync(lq => lq.LeaveQuotaId == id);
            if (leaveQuota == null)
            {
                return NotFound();
            }

            List<Employee>? employees = await _context.Employees.GetAllAsync() as List<Employee>;
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;

            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveQuota.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName", leaveQuota.LeaveTypeId);
            return View(leaveQuota);
        }

        // POST: LeaveQuotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveQuotaId,EmployeeId,LeaveTypeId,LeaveYear,TotalAllocated,TotalUsed")] LeaveQuota leaveQuota)
        {
            if (id != leaveQuota.LeaveQuotaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.LeaveQuotas.UpdateAsync(leaveQuota);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LeaveQuotaExists(leaveQuota.LeaveQuotaId))
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

            List<Employee>? employees = await _context.Employees.GetAllAsync() as List<Employee>;
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;

            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveQuota.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName", leaveQuota.LeaveTypeId);
            return View(leaveQuota);
        }

        // GET: LeaveQuotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveQuota? leaveQuota = await _context.LeaveQuotas.FindOneAsync(lq => lq.LeaveQuotaId == id, lq => lq.Employee, lq => lq.LeaveType);
            if (leaveQuota == null)
            {
                return NotFound();
            }

            return View(leaveQuota);
        }

        // POST: LeaveQuotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            LeaveQuota? leaveQuota = await _context.LeaveQuotas.FindOneAsync(lq => lq.LeaveQuotaId == id);
            if (leaveQuota != null)
            {
                await _context.LeaveQuotas.DeleteAsync(leaveQuota);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LeaveQuotaExists(int id)
        {
            return await _context.LeaveQuotas.ExistsAsync(lq => lq.LeaveQuotaId == id);
        }
    }
}
