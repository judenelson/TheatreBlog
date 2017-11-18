using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
/// <summary>
/// Post class for blogs
/// </summary>
namespace TheatreBlog.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string PostBody { get; set; }

        public List<Comment> Comments {get;set;}

        
        public string Author { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.Now;
    }
}