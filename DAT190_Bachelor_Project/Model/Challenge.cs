using System;
namespace DAT190_Bachelor_Project.Model
{
    public class Challenge
    {

        public string Image { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Task { get; set; }
        public int Goal { get; set; }
        public int Progress { get; set; }

        public Challenge(string image, string title, int goal, int progress, string color, string task, string description)
        {
            Image = image;
            Name = title;
            Color = color;
            Description = description;
            Task = task;
            Goal = goal;
            Progress = progress;
        }
    }
}
