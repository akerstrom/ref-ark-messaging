using System;

namespace RefArk.Station.Models
{
    public class TripEndedEventModel
    {
        public string CarID { get; set; }
        public int FuelLevel { get; set; }
        public int WiperFluidLevel { get; set; }
        public Position StartPosition { get; set; }
        public Position EndPosition { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Position
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
