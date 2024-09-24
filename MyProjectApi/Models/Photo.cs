namespace MyProjectApi.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public string Base64 { get; set; }
        public Car Car { get; set; }
    }
}
