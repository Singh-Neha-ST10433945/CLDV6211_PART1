using EventEaseBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class EventController : Controller
{
    private readonly AppDbContext _context;

    public EventController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Event
    public async Task<IActionResult> Index()
    {
        return View(await _context.Event.Include(e => e.Venue).ToListAsync());
    }

    // GET: Event/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var eventItem = await _context.Event.Include(e => e.Venue)
            .FirstOrDefaultAsync(m => m.EventId == id);
        if (eventItem == null)
            return NotFound();

        return View(eventItem);
    }

    // GET: Event/Create
    public IActionResult Create()
    {
        var venues = _context.Venue.ToList();
        ViewBag.VenueId = new SelectList(venues, "Id", "Name");
        ViewData["VenueId"] = new SelectList(venues, "VenueId", "VenueName");
        return View();
    }

    // POST: Event/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EventId,EventName,EventDate,VenueId,ImageUrl")] Event eventItem)
    {
       
        _context.Add(eventItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
       // ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "VenueName", eventItem.VenueId);
      //  return View(eventItem);
    }

    // GET: Event/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();
        var venues = _context.Venue.ToList();
        var eventItem = await _context.Event.FindAsync(id);
        if (eventItem == null)
            return NotFound();
       
        ViewBag.VenueId = new SelectList(venues, "Id", "Name");
        ViewData["VenueId"] = new SelectList(venues, "VenueId", "VenueName", eventItem.VenueId);
        return View(eventItem);
    }

    // POST: Event/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDate,VenueId,ImageUrl")] Event eventItem)
    {
        if (id != eventItem.EventId)
            return NotFound();

        
        _context.Update(eventItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
       // ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "VenueName", eventItem.VenueId);
        return View(eventItem);
    }

    // GET: Event/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var eventItem = await _context.Event.Include(e => e.Venue)
            .FirstOrDefaultAsync(m => m.EventId == id);
        if (eventItem == null)
            return NotFound();

        return View(eventItem);
    }

    // POST: Event/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Event eventItem)
    {
        if (id != eventItem.EventId)
        {
            return NotFound(); // If the venue is not found, return NotFound                     
        }

        _context.Event.Remove(eventItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EventExists(int id)
    {
        return _context.Event.Any(e => e.EventId == id);
    }
}
