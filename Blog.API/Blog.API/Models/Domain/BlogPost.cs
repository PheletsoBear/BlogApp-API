﻿/*
   This serves as the blueprint of defining the structure of the Entities named Category
   This is injecte into the DbContext class to migrate EF and create entity
 */


namespace Blog.API.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }//Database fiels named ID which is an uniques identifier
        public string Title { get; set; }          //Database field  of type string  Property
        public string ShortDesc { get; set; }      //Database field  of type string  Property
        public string Content { get; set; }        //Database field  of type string  Property
        public string FeaturedImgUrl { get; set; } //Database field  of type string  Property
        public string UrlHandle { get; set; }      //Database field  of type string  Property
        public DateTime PublishDate { get; set; }  //Database field  of type string  Property
        public string Author { get; set; }         //Database field  of type string  Property
        public bool IsVisible  { get; set; }       //Database field  of type string  Property

    }
}
