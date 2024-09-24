namespace MyProjectApi.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        // Foreign key for Photo
        public int? PhotoID { get; set; }
        public Photo Photo { get; set; } 
    }
}
