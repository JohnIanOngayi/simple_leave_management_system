using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Infrastructure.Repository;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly IRepositoryWrapper _context;

        public LeaveTypesController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;
            return View(leaveTypes);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveType? leaveType = await _context.LeaveTypes.FindOneAsync(m => m.LeaveTypeId == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveTypeId,LeaveTypeCode,LeaveTypeName,IsPaidLeave,IsActive")] LeaveType leaveType)
        {
            if (ModelState.IsValid)
            {
                await _context.LeaveTypes.CreateAsync(leaveType);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveType);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveType? leaveType = await _context.LeaveTypes.FindOneAsync(lt => lt.LeaveTypeId == id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveTypeId,LeaveTypeCode,LeaveTypeName,IsPaidLeave,IsActive")] LeaveType leaveType)
        {
            if (id != leaveType.LeaveTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.LeaveTypes.UpdateAsync(leaveType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LeaveTypeExists(leaveType.LeaveTypeId))
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
            return View(leaveType);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveType? leaveType = await _context.LeaveTypes.FindOneAsync(lt => lt.LeaveTypeId == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            LeaveType? leaveType = await _context.LeaveTypes.FindOneAsync(lt => lt.LeaveTypeId == id);
            if (leaveType != null)
            {
                await _context.LeaveTypes.DeleteAsync(leaveType);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LeaveTypeExists(int id)
        {
            return await _context.LeaveTypes.ExistsAsync(lt => lt.LeaveTypeId == id);
        }
    }
}
