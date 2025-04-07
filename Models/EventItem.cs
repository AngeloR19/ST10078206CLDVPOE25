namespace CLDVPOE25.Models
{
    public class EventItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfEvent { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string ContactEmail { get; set; }

        public List<Booking> Booking { get; set; } = new();
    }
}

