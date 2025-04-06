using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventEaseBookingSystem.Models;

public class Venue
{
    public int VenueId { get; set; }

    [Required]
    [StringLength(50)]
    public string VenueName { get; set; }

    [Required]
    [StringLength(50)]
    public string Location { get; set; }

    [Required]
    public int Capacity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;  // Default empty string for ImageUrl

    // Navigation Property for Events
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();  // Initialize as empty list

    // Constructor
    public Venue()
    {
        VenueName = string.Empty;  // Initialize with empty string
        Location = string.Empty;  // Initialize with empty string
    }
}
