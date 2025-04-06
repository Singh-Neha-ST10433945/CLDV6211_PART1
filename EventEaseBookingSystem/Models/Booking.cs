using System;
using System.ComponentModel.DataAnnotations;
using EventEaseBookingSystem.Models;

public class Booking
{
    // Primary key for the Booking entity
    public int BookingId { get; set; }

    // Foreign key to the Event entity (required)
    [Required]
    public int EventId { get; set; }

    // Foreign key to the Venue entity (required)
    [Required]
    public int VenueId { get; set; }

    // Date and time when the booking is made (required)
    [Required]
    public DateTime BookingDate { get; set; }

    // Navigation property to the related Event (nullable to prevent circular reference issues)
    public virtual Event? Event { get; set; }

    // Navigation property to the related Venue (nullable to prevent circular reference issues)
    public virtual Venue? Venue { get; set; }
}
