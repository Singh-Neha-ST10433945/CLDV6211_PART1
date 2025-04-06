using EventEaseBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
public class BookingController : Controller
{
    private readonly AppDbContext _context;

    // Constructor to initialize the database context
    public BookingController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Booking
    // Displays a list of all bookings with related event and venue data
    public async Task<IActionResult> Index()
    {
        var bookings = _context.Booking.Include(b => b.Event).Include(b => b.Venue);
        return View(await bookings.ToListAsync());
    }

    // GET: Booking/Details/5
    // Displays details for a specific booking
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var booking = await _context.Booking
            .Include(b => b.Event)
            .Include(b => b.Venue)
            .FirstOrDefaultAsync(m => m.BookingId == id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }

    // GET: Booking/Create
    // Returns the booking creation form with dropdowns for events and venues
    public IActionResult Create()
    {
        ViewBag.EventId = new SelectList(_context.Event, "EventId", "EventName");
        ViewBag.VenueId = new SelectList(_context.Venue, "VenueId", "VenueName");
        return View();
    }

    // POST: Booking/Create
    // Handles submission of a new booking
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BookingId,BookingDate,EventId,VenueId")] Booking booking)
    {
        if (ModelState.IsValid)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.EventId = new SelectList(_context.Event, "EventId", "EventName", booking.EventId);
        ViewBag.VenueId = new SelectList(_context.Venue, "VenueId", "VenueName", booking.VenueId);
        return View(booking);
    }

    // GET: Booking/Edit/5
    // Loads the edit form for a specific booking
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var booking = await _context.Booking.FindAsync(id);
        if (booking == null)
            return NotFound();

        ViewBag.EventId = new SelectList(_context.Event, "EventId", "EventName", booking.EventId);
        ViewBag.VenueId = new SelectList(_context.Venue, "VenueId", "VenueName", booking.VenueId);
        return View(booking);
    }

    // POST: Booking/Edit/5
    // Handles the update of an existing booking
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,EventId,VenueId")] Booking booking)
    {
        if (id != booking.BookingId)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.BookingId))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.EventId = new SelectList(_context.Event, "EventId", "EventName", booking.EventId);
        ViewBag.VenueId = new SelectList(_context.Venue, "VenueId", "VenueName", booking.VenueId);
        return View(booking);
    }

    // GET: Booking/Delete/5
    // Displays the delete confirmation view for a specific booking
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var booking = await _context.Booking
            .Include(b => b.Event)
            .Include(b => b.Venue)
            .FirstOrDefaultAsync(m => m.BookingId == id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }

    // POST: Booking/Delete/5
    // Confirms and deletes the selected booking
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking = await _context.Booking.FindAsync(id);
        if (booking == null)
            return NotFound();

        _context.Booking.Remove(booking);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Checks whether a booking exists in the database
    private bool BookingExists(int id)
    {
        return _context.Booking.Any(e => e.BookingId == id);
    }
}
