namespace CourierMvcApiConsume.Models
{
    public class TrackHistory
    {
        public int? Id { get; set; } 
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
