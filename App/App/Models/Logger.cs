using System;

namespace App.Models
{
    public class Logger
    {
        public double MinimumTemperature { get; set; }
        public Log Logs { get; set; }
        public Plant Plants { get; set; }

        public override string ToString()
        {
            return this.MinimumTemperature.ToString();
        }
    }
}
