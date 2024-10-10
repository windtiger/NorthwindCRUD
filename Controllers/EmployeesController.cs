using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthwindCRUD.Models;

namespace NorthwindCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly PubsContext _context;

        public EmployeesController(PubsContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var pubsContext = _context.Employees.Include(e => e.Job).Include(e => e.Pub);
            return View(await pubsContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Job)
                .Include(e => e.Pub)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId");
            ViewData["PubId"] = new SelectList(_context.Publishers, "PubId", "PubId");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,Fname,Minit,Lname,JobId,JobLvl,PubId,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employee.JobId);
            ViewData["PubId"] = new SelectList(_context.Publishers, "PubId", "PubId", employee.PubId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employee.JobId);
            ViewData["PubId"] = new SelectList(_context.Publishers, "PubId", "PubId", employee.PubId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpId,Fname,Minit,Lname,JobId,JobLvl,PubId,HireDate")] Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
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
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employee.JobId);
            ViewData["PubId"] = new SelectList(_context.Publishers, "PubId", "PubId", employee.PubId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Job)
                .Include(e => e.Pub)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
