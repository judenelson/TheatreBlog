using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreBlog.ViewModels
{
   
    
        public class UserViewModel
        {
            public string Id { get; set; }
            public bool IsAdmin { get; set; }
            public string Address { get; set; }
            public string FullName { get; set; }
            public bool IsSuspended { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
        }
    }