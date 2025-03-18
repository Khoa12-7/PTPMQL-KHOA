using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using System.Threading.Tasks;
using MvcMovie.Data;
using System.Linq;

namespace MvcMovie.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Persons.OfType<Employee>().ToListAsync();
            return View(employees);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,Gmail,EmployeeId,Age")] Employee newEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Persons.Add(newEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newEmployee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Persons.OfType<Employee>().FirstOrDefaultAsync(e => e.PersonId == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FullName,Address,Gmail,EmployeeId,Age")] Employee newEmployee)
        {
            if (id != newEmployee.PersonId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Persons.OfType<Employee>().AnyAsync(e => e.PersonId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newEmployee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Persons.OfType<Employee>().FirstOrDefaultAsync(e => e.PersonId == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Persons.OfType<Employee>().FirstOrDefaultAsync(e => e.PersonId == id);
            if (employee != null)
            {
                _context.Persons.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
