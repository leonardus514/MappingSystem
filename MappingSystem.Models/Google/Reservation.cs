namespace Google;

public sealed class Reservation
{
    public string? ExternalId { get; init; }
    public string? PrimaryGuest { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
}
