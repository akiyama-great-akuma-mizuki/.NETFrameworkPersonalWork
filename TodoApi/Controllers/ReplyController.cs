using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Service;
using Server.Entity;

namespace Server.Controller
{
    [ApiController]
    [Route("reply")]
    public class ReplyController : ControllerBase
    {
        public class TempReply
        {
            public string UserId { get; set; }
            public int PostId { get; set; }
            public string Content { get; set; }
            public string time { get; set; }
        }
        //业务层依赖
        private readonly IEnumerable<IService> _services;
        private readonly ReplyService replyService;
        public ReplyController(IEnumerable<IService> services)
        {
            _services = services;
            replyService = _services.First(m => m.ServiceName == nameof(ReplyService)) as ReplyService;
        }
        [HttpPost("add")]
        public void AddReply(TempReply reply)
        {
            long timestamp = long.Parse(reply.time);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(timestamp);
            replyService.AddReply(reply.PostId,reply.UserId,reply.Content,dt);
        }

        [HttpGet("getreplybyid")]
        public ActionResult<TempReply> getReplyById(int id)
        {
            Reply m = replyService.GetReplyByReplyId(id);
            return new TempReply
            {
                UserId = m.User.UserId,
                PostId = m.Post.PostId,
                Content = m.Content,
                time = m.DateTime.ToString()
            };
        }

        [HttpGet("getreplybyuserid")]
        public ActionResult<List<TempReply>> getRepliesByUserId(string id)
        {
            return ConvertToTempReply(replyService.GetRepliesByUserId(id));
        }

        [HttpDelete("delete")]
        public void DeleteReply(int id)
        {
            replyService.DeleteReply(id);
        }

        public static List<TempReply> ConvertToTempReply(List<Reply> replies)
        {
            List<TempReply> res = new List<TempReply>();
            replies.ForEach(m => res.Add(new TempReply
            {
                UserId = m.User.UserId,
                PostId = m.Post.PostId,
                Content = m.Content,
                time = m.DateTime.ToString()
            }));
            return res;
        }
    }
}