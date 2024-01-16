namespace BB_01._15._2024_Template.Areas.BBAdmin.ViewModels.News
{
    public class CreateNewsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ByName { get; set; }
        public IFormFile Photo { get; set; }
    }
}
