namespace BB_01._15._2024_Template.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ByName { get; set; }
        public string Image { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
