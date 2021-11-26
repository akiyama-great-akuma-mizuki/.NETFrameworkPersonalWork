using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Service;
using Server.Entity;
using Microsoft.AspNetCore.Cors;

namespace Server.Controller
{
    [ApiController]
    [Route("post")]
    public class PostController : ControllerBase
    {
        public class TempPost
        {
            public string uid { get; set; }
            public int pid { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string time { get; set; }
            public string DT { get; set; }
            public List<ReplyController.TempReply> Replies { get; set; } 
        }
        //业务层依赖
        private readonly IEnumerable<IService> _services;
        private readonly PostService postService;
        public PostController(IEnumerable<IService> services)
        {
            _services = services;
            postService = _services.First(m =>
                   m.ServiceName == nameof(PostService))
                    as PostService;
        }

        //查询
        [HttpGet("count")]
        public ActionResult<int> GetPostsCount()
        {
            return postService.GetPostsCount();
        }
        [HttpGet("getpostbyid")]
        public ActionResult<TempPost> GetPostById(int pid)
        {

            Post post = postService.GetPostById(pid);
            if (post != null)
                return new TempPost {pid = post.PostId,uid = post.User.UserId, title = post.Title, content = post.Content, Replies = ReplyController.ConvertToTempReply( post.Replies ), DT = post.DateTime.ToString() };
            else return new BadRequestResult();
        }
        [HttpGet("getpostbyuid")]
        public List<TempPost> GetPostByUid(string uid)
        {

            return ConvertToTempPost(postService.GetPostsByUserId(uid));
        }
        [HttpGet("getpostbykeyword")]
        public List<TempPost> GerPostsByKeyword(string key)
        {

            return ConvertToTempPost(postService.GetPostsByKeyword(key));
        }
        [HttpGet("allposts")]
        public List<TempPost> GetPosts(int length, int number)
        {
            return ConvertToTempPost(postService.GetPosts(length, number));
        }
        //增加
        [HttpPost("addpost")]
        public void AddPost(TempPost tp)
        {

            long timestamp = long.Parse(tp.time);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(timestamp);

            postService.AddPost(tp.uid, tp.title, tp.content, dt);
        }

        //修改
        [HttpPost("resetpost")]
        public void ResetPost(TempPost tp)
        {
            long timestamp = long.Parse(tp.time);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(timestamp);

            postService.ModifyPost(tp.pid, tp.title, tp.content, dt);
        }

        //删除
        [HttpDelete("removepost")]
        public void DeletePost(int pid)
        {
            postService.RemovePostById(pid);
        }
        //收藏

        [HttpGet("collectpost")]
        public void Collect(string uid,int pid)
        {
            postService.CollectPost(uid, pid);
        }

        private List<TempPost> ConvertToTempPost(List<Post> posts)
        {
            
            List<TempPost> res = new List<TempPost>();
            posts.ForEach(m =>
            {
                res.Add(new TempPost { pid = m.PostId, uid = m.User.UserId, title = m.Title, content = m.Content, DT = m.DateTime.ToString(), Replies = ReplyController.ConvertToTempReply(m.Replies) });
            });
            return res;
        }
    }
}