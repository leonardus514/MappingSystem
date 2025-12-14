namespace Model;

public sealed class Reservation
{
    public string? Id { get; init; }
    public string? GuestName { get; init; }
    public DateTime From { get; init; }
    public DateTime To { get; init; }
}
