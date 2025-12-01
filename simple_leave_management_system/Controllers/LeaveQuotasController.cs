using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Infrastructure;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Controllers
{
    public class LeaveQuotasController : Controller
    {
        private readonly RepositoryContext _context;

        public LeaveQuotasController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: LeaveQuotas
        public async Task<IActionResult> Index()
        {
            var repositoryContext = _context.LeaveQuotas.Include(l => l.Employee).Include(l => l.LeaveType);
            return View(await repositoryContext.ToListAsync());
        }

        // GET: LeaveQuotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveQuota = await _context.LeaveQuotas
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.LeaveQuotaId == id);
            if (leaveQuota == null)
            {
                return NotFound();
            }

            return View(leaveQuota);
        }

        // GET: LeaveQuotas/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeCode");
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
                _context.Add(leaveQuota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode", leaveQuota.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeCode", leaveQuota.LeaveTypeId);
            return View(leaveQuota);
        }

        // GET: LeaveQuotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveQuota = await _context.LeaveQuotas.FindAsync(id);
            if (leaveQuota == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode", leaveQuota.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeCode", leaveQuota.LeaveTypeId);
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
                    _context.Update(leaveQuota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveQuotaExists(leaveQuota.LeaveQuotaId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode", leaveQuota.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeCode", leaveQuota.LeaveTypeId);
            return View(leaveQuota);
        }

        // GET: LeaveQuotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveQuota = await _context.LeaveQuotas
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.LeaveQuotaId == id);
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
            var leaveQuota = await _context.LeaveQuotas.FindAsync(id);
            if (leaveQuota != null)
            {
                _context.LeaveQuotas.Remove(leaveQuota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveQuotaExists(int id)
        {
            return _context.LeaveQuotas.Any(e => e.LeaveQuotaId == id);
        }
    }
}
