using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models
{
    public class TrackHistoryApi
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }

        public int UserId { get; set; }
        public UserApi? User { get; set; }
    }
}
