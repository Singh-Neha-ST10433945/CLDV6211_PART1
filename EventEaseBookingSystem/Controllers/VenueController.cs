using EventEaseBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class VenueController : Controller
{
    private readonly AppDbContext _context;

    public VenueController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Venue
    public async Task<IActionResult> Index()
    {
        return View(await _context.Venue.ToListAsync());
    }

    // GET: Venue/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var venue = await _context.Venue.FirstOrDefaultAsync(m => m.VenueId == id);
        if (venue == null)
            return NotFound();

        return View(venue);
    }

    // GET: Venue/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Venue/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VenueId,VenueName,Location,Capacity,ImageUrl")] Venue venue)
    {
        if (ModelState.IsValid)
        {
            _context.Add(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(venue);
    }

    // GET: Venue/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var existingVenue = await _context.Venue.FindAsync(id);
        if (existingVenue == null)
            return NotFound();

        return View(existingVenue);
    }

    // POST: Venue/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Venue venue)
    {
        if (id != venue.VenueId)
            return NotFound();

        // Debugging: Log the venue object data to check if the model is updated
        Console.WriteLine($"Venue Name: {venue.VenueName}, Location: {venue.Location}, Capacity: {venue.Capacity}, ImageUrl: {venue.ImageUrl}");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Venue.Update(venue); // Update the venue object
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the index page
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(venue.VenueId))
                    return NotFound();
                else
                    throw;
            }
        }
        return View(venue);
    }

    // GET: Venue/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var venue = await _context.Venue.FindAsync(id);
        if (venue == null)
            return NotFound();

        return View(venue);
    }

    // POST: Venue/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Venue venue)
    {

        if (id != venue.VenueId)
        {
          return NotFound(); // If the venue is not found, return NotFound                     
        }


        _context.Venue.Remove(venue); // Update the venue object
        await _context.SaveChangesAsync(); // Save changes to the database
        return RedirectToAction(nameof(Index)); // Redirect to the index page


    }

    private bool VenueExists(int id)
    {
        return _context.Venue.Any(e => e.VenueId == id);
    }
}
