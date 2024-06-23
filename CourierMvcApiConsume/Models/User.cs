namespace CourierMvcApiConsume.Models
{
    public class User
    {
        public int Id { get; set; }
        public string TrackId { get; set; }
        public string Description { get; set; }
        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public DateTime Created { get; set; }

        public string SenderName { get; set; }
        public string SenderContact { get; set; }
        public string SenderAddress { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverContact { get; set; }
        public string ReceiverAddress { get; set; }

        public ICollection<TrackHistory>? TrackHistory { get; set; }
    }
}
