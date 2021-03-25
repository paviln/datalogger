namespace App.Models
{
    public class Log
    {
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double SoilHumidity { get; set; }
        public string PlantId { get; set; }
        public override string ToString()
        {
            return this.Temperature.ToString() + this.AirHumidity.ToString() + this.SoilHumidity.ToString() + this.PlantId;
        }
    }
}
