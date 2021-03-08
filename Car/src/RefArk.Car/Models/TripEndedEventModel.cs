using System;

namespace RefArk.Car.Models
{
    public class TripEndedEventModel
    {
        public string CarID { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
