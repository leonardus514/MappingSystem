using System;
using MappingSystem.Core;
using MappingSystem.Mappings;

var handler = new MapHandler(new IObjectMapper[]
{
    new GoogleToReservationMapper(),
    new ReservationToGoogleMapper()
});

var modelReservation = new Model.Reservation
{
    Id = "RES-2025-001",
    GuestName = "Angela Merkel",
    From = new DateTime(2025, 3, 15),
    To = new DateTime(2025, 3, 18)
};

Console.WriteLine($"Source type: {modelReservation.GetType().FullName}");

var googleReservation = (Google.Reservation)
    handler.Map(modelReservation, "Model.Reservation", "Google.Reservation");

Console.WriteLine($"Mapped type: {googleReservation.GetType().FullName}");
Console.WriteLine($"Google: {googleReservation.ExternalId}, {googleReservation.PrimaryGuest}");

var backToModel = (Model.Reservation)
    handler.Map(googleReservation, "Google.Reservation", "Model.Reservation");

Console.WriteLine($"Model: {backToModel.Id}, {backToModel.GuestName}");

Console.WriteLine();
Console.WriteLine("=== Error handling demo ===");

try
{
    handler.Map(modelReservation, "Model.Reservation", "PartnerX.Reservation");
}
catch (MappingNotFoundException ex)
{
    Console.WriteLine(ex.Message);
}
