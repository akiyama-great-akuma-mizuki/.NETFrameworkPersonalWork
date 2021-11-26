using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Server.Entity;
using System.Linq;
using Server.DAO;
using System;

/**
 * 查询：1.根据用户id查询
 *      3.根据PostId查询
 *      2.根据标题关键字查询
 * 增加：根据用户id查询用户对象，再修改参数中的Post对象
 * 删除：级联删除reply
 * 修改：根据传入的参数post对象修改
 */
namespace Server.Service
{
    public class PostService : IService
    {

        public string ServiceName => nameof(PostService);
        private ServerContext context;

        public PostService(ServerContext context)
        {
            this.context = context;
        }

        //查询
        public int GetPostsCount()
        {
            return context.Posts.Count();
        }
        public Post GetPostById(int id)
        {
            return context.Posts.Include("Replies").Include("Replies.User").Include("User").Where(m => m.PostId == id).FirstOrDefault() as Post;
        }

        public List<Post> GetPostsByUserId(string id)
        {
            return context.Posts.Include("Replies").Include("Replies.User").Include("User").Where(m => m.User.UserId == id).ToList() as List<Post>;
        }

        public List<Post> GetPostsByKeyword(String pattern)
        {
            return context.Posts.Include("Replies").Include("Replies.User").Include("User").Where(m => m.Title.Contains(pattern)).ToList() as List<Post>;
        }

        public List<Post> GetPosts(int l, int n)
        {
            return context.Posts.Include("Replies").Include("Replies.User").Include("User").Skip((n - 1) * l).Take(l).ToList() as List<Post>;
        }
        //增加
        public void AddPost(string uid,string title,string content,DateTime dateTime)
        {
            var user = context.Users.Where(m => m.UserId == uid).FirstOrDefault();
            Post post = new Post()
            {
                User = user,
                Title = title,
                Content = content,
                DateTime = dateTime,
                Replies = new List<Reply>()
            };
            context.Add(post);
            context.SaveChanges();
        }

        //修改
        public void ModifyPost(int id, string title,string content,DateTime time)
        {
            var res = context.Posts.Where(m => m.PostId == id).FirstOrDefault();
            if(res != null)
            {
                res.Title = title;
                res.Content = content;
                res.DateTime = time;
                context.SaveChanges();
            }
        }

        //删除
        public void RemovePostById(int id)
        {
            var res = context.Posts.Include("Replies")
                                    .FirstOrDefault(m => m.PostId == id);
            if(res != null)
            {
                context.Remove(res);
                context.SaveChanges();
            }
        }
        //收藏
        public void CollectPost(string uid,int postid)
        {
            var user = context.Users.FirstOrDefault(m => m.UserId == uid);
            var post = context.Posts.FirstOrDefault(m => m.PostId == postid);
            if(user != null && post != null)
            {
                user.AllPosts.Add(post);
            }
        }
    }
}