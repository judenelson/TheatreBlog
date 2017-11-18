using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheatreBlog.Models
{   /// <summary>
/// this class is for the user comments
/// it links to the application user table in the asp.net membership database
/// </summary>
    public class Comment
    {
       [Key]
       //this will be the primary key
       public int  CommentID { get; set; }

       [Required]
       //this will be the foreign key to the post class
        public int PostID { get; set; }

       [Required]
       [DataType(DataType.MultilineText)]

       public string CommentText { get; set; }

        //this will link to the username field in the database
       public string Author { get; set; }

        //this will store the datetime with a default of current datetime
        public DateTime CommentDate { get; set; } = DateTime.Now;
    }
}