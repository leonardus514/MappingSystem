using System;
using MappingSystem.Core;

namespace MappingSystem.Mappings;

public sealed class ReservationToGoogleMapper : IObjectMapper
{
    public string SourceType => "Model.Reservation";
    public string TargetType => "Google.Reservation";

    public object Map(object source)
    {
        if (source is not Model.Reservation r)
            throw new ArgumentException(
                $"Expected {SourceType} but got {source.GetType().FullName}."
            );

        return new Google.Reservation
        {
            ExternalId = $"GOOGLE-{r.Id}",
            PrimaryGuest = r.GuestName?.ToUpperInvariant(),
            CheckIn = r.From,
            CheckOut = r.To
        };
    }
}
