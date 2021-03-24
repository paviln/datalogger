using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class PlantPOCO
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string loggerId { get; set; }
        public SoilType soilType { get; set; }
        public byte[] img { get; set; }
        public Status status { get; set; }
        public Log[] logs { get; set; }
    }
}
