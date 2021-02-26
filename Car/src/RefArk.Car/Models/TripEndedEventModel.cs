using System;

namespace RefArk.Customer.Models
{
    public class TripEndedEventModel
    {
        public string CarID { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
