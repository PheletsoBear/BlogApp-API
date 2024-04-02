using Blog.API.Models.Domain;

namespace Blog.API.Models.DTO.BlogPost
{
    public class UpdateBlogPostDTO
    {
        public string Title { get; set; }          //Database field  of type string  Property
        public string ShortDesc { get; set; }      //Database field  of type string  Property
        public string Content { get; set; }        //Database field  of type string  Property
        public string FeaturedImgUrl { get; set; } //Database field  of type string  Property
        public string UrlHandle { get; set; }      //Database field  of type string  Property
        public DateTime PublishDate { get; set; }  //Database field  of type string  Property
        public string Author { get; set; }         //Database field  of type string  Property
        public bool IsVisible { get; set; }       //Database field  of type string  Property
        public Guid[] Categories { get; set; }
    }
}
