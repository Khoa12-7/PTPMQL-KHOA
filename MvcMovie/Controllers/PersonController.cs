using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var persons = await _context.Persons.ToListAsync();  // Lấy dữ liệu từ DB
            return View(persons);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,Gmail,PersonType")] Person person)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ Dữ liệu không hợp lệ");
                return View(person);
            }

            try
            {
                _context.Persons.Add(person);
                await _context.SaveChangesAsync(); // 🔥 Đảm bảo gọi SaveChangesAsync()
                Console.WriteLine($"✅ Thêm thành công: {person.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi thêm: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var person = await _context.Persons.FindAsync(id);
            if (person == null) return NotFound();

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FullName,Address,Gmail,PersonType")] Person person)
        {
            if (id != person.PersonId) return NotFound();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation Error: {error}");
                }
                return View(person);
            }

            try
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonId)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null) return NotFound();

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }
}