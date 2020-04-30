using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4._27.Data
{
    public class PostsRepository
    {
        private readonly string _connectionString;

        public PostsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Post> GetPosts()
        {
            using (var context = new PostsContext(_connectionString))
            {
                return context.Posts.OrderByDescending(p => p.DateSubmitted).ToList();
            }
        }
        public void AddPost(Post post)
        {
            using (var context = new PostsContext(_connectionString))
            {
                context.Posts.Add(post);
                context.SaveChanges();
            }
        }
        public Post GetById(int id)
        {
            using (var context = new PostsContext(_connectionString))
            {
                return context.Posts.FirstOrDefault(p => p.Id == id);
            }
        }
        public void AddLike(int Id)
        {
            using (var context = new PostsContext(_connectionString))
            {
                context.Posts.FirstOrDefault(p => p.Id == Id).Likes++;
                context.SaveChanges();
            }
        }
        public int GetLikesCount(int Id)
        {
            using (var context = new PostsContext(_connectionString))
            {
                var post = context.Posts.FirstOrDefault(p => p.Id == Id);
                return post.Likes;
            }
        }
    }
}
