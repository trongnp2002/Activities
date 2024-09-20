namespace Domain;

public class UserBooking
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAllDay { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string RecurrenceRule { get; set; }
}