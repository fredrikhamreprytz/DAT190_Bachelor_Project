using System;
namespace DAT190_Bachelor_Project.Model
{
    public class DataSource
    {
        DateTime Timestamp;
        string Description;
        double Value;
        EmissionType Type;

        public DataSource(DateTime timestamp, string description, double value, EmissionType type)
        {
            this.Timestamp = timestamp;
            this.Description = description;
            this.Value = value;
            this.Type = type;
        }


    }
}
