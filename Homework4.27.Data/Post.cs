using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework4._27.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
