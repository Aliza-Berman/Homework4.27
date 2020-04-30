using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Homework4._27.Models;
using Microsoft.Extensions.Configuration;
using Homework4._27.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Homework4._27.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            var repo = new PostsRepository(_connectionString);
            return View(repo.GetPosts());
            
        }
        public IActionResult AddPost()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(string title,string image)
        {
            var repo = new PostsRepository(_connectionString);
            var post = new Post
            {
                Title = title,
                Image = image,
                DateSubmitted = DateTime.Now
            };                  
            repo.AddPost(post);
            return Redirect("/");
        }
        public IActionResult ViewPost(int id)
        {
            var repos = new PostsRepository(_connectionString);
            var post = repos.GetById(id);
            return View(post);
        }
        [HttpPost]
        public IActionResult AddLikes(int id)
        {
            var repo = new PostsRepository(_connectionString);
            List<int> likes = HttpContext.Session.Get<List<int>>("Likes");
            if (likes == null)
            {
                likes = new List<int>();
            }
            else if (likes.Contains(id))
            {
                return null;
            }
            likes.Add(id);
            HttpContext.Session.Set("Likes", likes);
            var post = repo.GetById(id);
            repo.AddLike(id);
            return Json(post);
        }
        public IActionResult GetLikes(int id)
        {
            var repo = new PostsRepository(_connectionString);
            var post = repo.GetById(id);
            post.Likes =  repo.GetLikesCount(id);
            return Json(post);
        }
        
    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
