using System;
using Newtonsoft.Json;

namespace RefArk.Car.Models
{
    public class WaypointModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid WaypointID { get; set; }
        public string CarID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
