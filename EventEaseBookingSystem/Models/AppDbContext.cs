using Microsoft.EntityFrameworkCore;
using EventEaseBookingSystem.Models;

// Represents the database context for the EventEase Booking System
public class AppDbContext : DbContext
{
    // Constructor that passes options (like connection string) to the base DbContext class
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSet represents the Venues table in the database
    public DbSet<Venue> Venue { get; set; }

    // DbSet represents the Events table in the database
    public DbSet<Event> Event { get; set; }

    // DbSet represents the Bookings table in the database
    public DbSet<Booking> Booking { get; set; }
}
