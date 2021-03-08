using System;

namespace RefArk.Customer.Models
{
    public class TripEndedEventModel
    {
        public string CarID { get; set; }
        public int FuelLevel { get; set; }
        public int WiperFluidLevel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
