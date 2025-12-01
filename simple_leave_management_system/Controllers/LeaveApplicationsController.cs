using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Infrastructure.Repository;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly IRepositoryWrapper _context;

        public LeaveApplicationsController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            List<LeaveApplication>? leaveApplications = await _context.LeaveApplications.GetAllAsync(a => a.Employee, a => a.LeaveType) as List<LeaveApplication>;
            return View(leaveApplications);
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveApplication? leaveApplication = await _context.LeaveApplications.FindOneAsync(a => a.LeaveApplicationId == id,
                a => a.Approver,
                a => a.Employee,
                a => a.LeaveType);

            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public async Task<IActionResult> Create()
        {
            List<Employee>? employees = await _context.Employees.GetAllAsync() as List<Employee>;
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;
            var allowedStatus = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Pending", Text = "Pending" },
                    new SelectListItem { Value = "Approved", Text = "Approved", Selected = true },
                    new SelectListItem { Value = "Rejected", Text = "Rejected" },
                    new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
                };

            ViewData["Status"] = new SelectList(allowedStatus, "Value", "Text");

            ViewData["ApprovedBy"] = new SelectList(employees, "EmployeeId", "DisplayText");
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText");
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName");
            return View();
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveApplicationId,EmployeeId,LeaveTypeId,FromDate,ToDate,TotalDays,Reason,Status,AppliedOn,ApprovedBy,ApprovedOn")] LeaveApplication leaveApplication)
        {
            if (ModelState.IsValid)
            {
                await _context.LeaveApplications.CreateAsync(leaveApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<Employee>? employees = await _context.Employees.GetAllAsync() as List<Employee>;
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;
            var allowedStatus = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Pending", Text = "Pending" },
                    new SelectListItem { Value = "Approved", Text = "Approved", Selected = true },
                    new SelectListItem { Value = "Rejected", Text = "Rejected" },
                    new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
                };

            ViewData["Status"] = new SelectList(allowedStatus, "Value", "Text");

            ViewData["ApprovedBy"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveApplication.ApprovedBy);
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName", leaveApplication.LeaveTypeId);

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindOneAsync(a => a.LeaveApplicationId == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            List<Employee>? employees = await _context.Employees.GetAllAsync() as List<Employee>;
            List<LeaveType>? leaveTypes = await _context.LeaveTypes.GetAllAsync() as List<LeaveType>;

            var allowedStatus = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Pending", Text = "Pending" },
                    new SelectListItem { Value = "Approved", Text = "Approved"},
                    new SelectListItem { Value = "Rejected", Text = "Rejected" },
                    new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
                };

            ViewData["Status"] = new SelectList(allowedStatus, "Value", "Text", leaveApplication.Status);
            ViewData["ApprovedBy"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveApplication.ApprovedBy);
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName", leaveApplication.LeaveTypeId);

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveApplicationId,EmployeeId,LeaveTypeId,FromDate,ToDate,TotalDays,Reason,Status,AppliedOn,ApprovedBy,ApprovedOn")] LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.LeaveApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.LeaveApplications.UpdateAsync(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LeaveApplicationExists(leaveApplication.LeaveApplicationId))
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
            var allowedStatus = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Pending", Text = "Pending" },
                    new SelectListItem { Value = "Approved", Text = "Approved"},
                    new SelectListItem { Value = "Rejected", Text = "Rejected" },
                    new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
                };

            ViewData["Status"] = new SelectList(allowedStatus, "Value", "Text", leaveApplication.Status);
            ViewData["ApprovedBy"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveApplication.ApprovedBy);
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "DisplayText", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "LeaveTypeId", "LeaveTypeName", leaveApplication.LeaveTypeId);

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveApplication? leaveApplication = await _context.LeaveApplications.FindOneAsync(a => a.LeaveApplicationId == id,
                a => a.Approver,
                a => a.Employee,
                a => a.LeaveType);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindOneAsync(a => a.LeaveApplicationId == id);
            if (leaveApplication != null)
            {
                await _context.LeaveApplications.DeleteAsync(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LeaveApplicationExists(int id)
        {
            return await _context.LeaveApplications.ExistsAsync(a => a.LeaveApplicationId == id);
        }
    }
}
