using System;
using System.ComponentModel.DataAnnotations;

namespace EventEaseBookingSystem.Models;

public class Event
{
    // Primary Key
    public int EventId { get; set; }

    // Event name is required and limited to 50 characters
    [Required]
    [StringLength(50)]
    public string EventName { get; set; }

    // Event date is required
    [Required]
    public DateTime EventDate { get; set; }

    // Description of the event is required
    [Required]
    public string Description { get; set; }

    // Foreign Key: ID of the venue associated with the event
    public int VenueId { get; set; }

    // Navigation property linking the event to its venue
    public virtual Venue Venue { get; set; } = null!; // Will be set by EF during runtime

    // Constructor: Ensures strings are initialized to prevent null-related issues
    public Event()
    {
        EventName = string.Empty;
        Description = string.Empty;
    }
}
