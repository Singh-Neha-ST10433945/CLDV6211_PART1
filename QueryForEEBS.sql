DROP TABLE IF EXISTS Booking;
DROP TABLE IF EXISTS Event;
DROP TABLE IF EXISTS Venue;

CREATE TABLE Venue(
    VenueId INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    VenueName VARCHAR (50) NOT NULL,
    Location VARCHAR (50) NOT NULL,
    Capacity INT NOT NULL,
    ImageUrl VARCHAR (MAX)
);

CREATE TABLE Event(
    EventId INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    EventName VARCHAR (50) NOT NULL,
    EventDate DATE NOT NULL,
    Description VARCHAR (MAX) NOT NULL,
    VenueId INT NOT NULL,
    FOREIGN KEY (VenueId) REFERENCES Venue (VenueId)
);

CREATE TABLE Booking(
    BookingId INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    EventId INT NOT NULL,
    VenueId INT NOT NULL,
    BookingDate DATE NOT NULL,
    FOREIGN KEY (VenueId) REFERENCES Venue (VenueId),
    FOREIGN KEY (EventId) REFERENCES Event (EventId)
);

-- Insert into Venue table first to ensure VenueId exists
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)
VALUES ('Hall 1', 'ROOEPOORT', 100, 'image1.jpg');

-- Check the VenueId generated for the inserted Venue
SELECT * FROM Venue;

-- Insert into Event table with an existing VenueId
INSERT INTO Event(EventName, EventDate, Description, VenueId)
VALUES ('Wedding', '2025-10-02', 'Slay', 1);  -- Use valid VenueId, e.g., 1

-- Insert into Booking table with an existing EventId and VenueId
INSERT INTO Booking(EventId, VenueId, BookingDate)
VALUES (1, 1, '2025-12-08');  -- Use valid EventId and VenueId

SELECT * FROM Venue;
SELECT * FROM Event;
SELECT * FROM Booking;