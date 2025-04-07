using CLDVPOE25.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLDVPOE25.Controllers
{
    public class EventItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eventItems = await _context.EventItem.ToListAsync();
            return View(eventItems);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var eventItem = await _context.EventItem.FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(EventItem eventItem)
        {
            if (ModelState.IsValid)
            {
                // This will store only the date part, setting time to 00:00:00
                eventItem.DateOfEvent = eventItem.DateOfEvent.Date;

                _context.Add(eventItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventItem);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var eventItem = await _context.EventItem.FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int? id)
        {
            var eventItem = await _context.EventItem.FindAsync(id);
            _context.EventItem.Remove(eventItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public bool EventItemExists(int id)
        {
            return _context.EventItem.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.EventItem.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventItem eventItem)
        {
            if (id != eventItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    eventItem.DateOfEvent = eventItem.DateOfEvent.Date;

                    _context.Update(eventItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventItemExists(eventItem.Id))
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
            return View(eventItem);
        }
    }
}


