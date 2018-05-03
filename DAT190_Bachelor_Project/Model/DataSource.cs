using System;
namespace DAT190_Bachelor_Project.Model
{
    public class DataSource
    {
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public EmissionType Type { get; set; }

        public DataSource(DateTime timestamp, string description, double value, EmissionType type)
        {
            this.Timestamp = timestamp;
            this.Description = description;
            this.Value = value;
            this.Type = type;
        }
    }
}
