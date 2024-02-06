namespace Blog.API.Models.DTO
{
    public class CreateBlogPostDTO
    {
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Content { get; set; }
        public string FeaturedImgUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        public Guid[] Categories { get; set; }


    }
}
