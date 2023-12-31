﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefArk.Customer.Models
{
    public class CustomerModel
    {
        [JsonProperty(PropertyName = "customerId")]
        public string CustomerID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "noOfTrips")]
        public int NoOfTrips { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
