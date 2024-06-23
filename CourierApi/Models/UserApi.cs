using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class UserApi
    {
        public int Id { get; set; }
        public string TrackId { get; set; }
        public string Description { get; set; }
        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public DateTime Created { get; set; }

        public string SenderName { get; set; }
        public string SenderContact { get; set;}
        public string SenderAddress { get; set;}

        public string ReceiverName { get; set; }
        public string ReceiverContact { get; set; }
        public string ReceiverAddress { get; set; }

       public ICollection<TrackHistoryApi>? TrackHistory { get; set; }


    }
}
