using CLDVPOE25.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLDVPOE25.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Booking
                .Include(i => i.Venue)
                .Include(i => i.EventItem)
                .ToListAsync();
            return View(bookings);
        }
        public IActionResult Create()
        {
            ViewBag.Venue = _context.Venue.ToList();
            ViewBag.EventItem = _context.EventItem.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Venue = _context.Venue.ToList();
            ViewBag.EventItem = _context.EventItem.ToList();
            return View(booking);
        }

        public async Task<IActionResult> Duration(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if(booking == null) return NotFound();

            return View(booking);
        }
        [HttpPost]
        public async Task<IActionResult> Duration(int id, Booking model)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            booking.Duration = model.Duration;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Feedback(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }
        [HttpPost]
        public async Task<IActionResult> Feedback(int id, Booking model)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            booking.Feedback = model.Feedback;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
