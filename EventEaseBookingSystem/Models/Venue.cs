using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventEaseBookingSystem.Models;

public class Venue
{
    // Primary Key
    public int VenueId { get; set; }

    // Venue name is required and limited to 50 characters
    [Required]
    [StringLength(50)]
    public string VenueName { get; set; }

    // Location of the venue is required and limited to 50 characters
    [Required]
    [StringLength(50)]
    public string Location { get; set; }

    // Capacity is required (number of people venue can hold)
    [Required]
    public int Capacity { get; set; }

    // URL or path to an image of the venue
    public string ImageUrl { get; set; } = string.Empty; // Initialized to avoid null

    // Navigation Property - one venue can host multiple events
    public virtual ICollection<Event> Events { get; set; } = new List<Event>(); // Ensures it's never null

    // Constructor - ensures string fields are initialized
    public Venue()
    {
        VenueName = string.Empty;
        Location = string.Empty;
    }
}
