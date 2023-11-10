/*
   This serves as the blueprint of defining the structure of the Entities named Category
   This is injecte into the DbContext class to migrate EF and create entity
 */

namespace Blog.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; } //Database fiels named ID which is an uniques identifier
        public string Name { get; set; } //Database field named Name of type string
        public string UrlHandle { get; set; } //Database field named UrlHandle of type string

    }
}
