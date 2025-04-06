using EventEaseBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

// Controller for handling Venue-related actions
public class VenueController : Controller
{
    private readonly AppDbContext _context;

    // Constructor: Injects the application's database context
    public VenueController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Venue
    // Displays a list of all venues
    public async Task<IActionResult> Index()
    {
        return View(await _context.Venue.ToListAsync());
    }

    // GET: Venue/Details/5
    // Displays detailed information about a specific venue
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
    // Displays the form to create a new venue
    public IActionResult Create()
    {
        return View();
    }

    // POST: Venue/Create
    // Adds a new venue to the database
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
    // Displays the form to edit an existing venue
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
    // Updates an existing venue's information in the database
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Venue venue)
    {
        if (id != venue.VenueId)
            return NotFound();

        // Logs venue data to the console for debugging
        Console.WriteLine($"Venue Name: {venue.VenueName}, Location: {venue.Location}, Capacity: {venue.Capacity}, ImageUrl: {venue.ImageUrl}");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Venue.Update(venue); // Update venue in the database context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect back to venue list
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
    // Displays confirmation page for deleting a venue
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
    // Deletes a venue from the database
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Venue venue)
    {
        if (id != venue.VenueId)
        {
            return NotFound(); // If venue IDs don't match, return 404
        }

        _context.Venue.Remove(venue); // Remove venue from database
        await _context.SaveChangesAsync(); // Save changes
        return RedirectToAction(nameof(Index)); // Redirect to venue list
    }

    // Helper method to check if a venue exists by ID
    private bool VenueExists(int id)
    {
        return _context.Venue.Any(e => e.VenueId == id);
    }
}
