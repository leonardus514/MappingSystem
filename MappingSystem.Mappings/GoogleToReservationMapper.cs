using System;
using MappingSystem.Core;

namespace MappingSystem.Mappings;

public sealed class GoogleToReservationMapper : IObjectMapper
{
    public string SourceType => "Google.Reservation";
    public string TargetType => "Model.Reservation";

    public object Map(object source)
    {
        if (source is not Google.Reservation r)
            throw new ArgumentException(
                $"Expected {SourceType} but got {source.GetType().FullName}."
            );

        return new Model.Reservation
        {
            Id = r.ExternalId?.Replace("GOOGLE-", ""),
            GuestName = r.PrimaryGuest,
            From = r.CheckIn,
            To = r.CheckOut
        };
    }
}
