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
 * 查询：
 *      1.根据Userid
 *      2.根据Postid
 * 增加：
 *      1.根据Userid,Postid和content生成对象
 * 删除：
 *      1.根据replyid删除
 * 修改
 *      1.根据replyid修改
 */
namespace Server.Service
{
    public class ReplyService : IService
    {
        public string ServiceName => nameof(ReplyService);
        private ServerContext context;

        public ReplyService(ServerContext context)
        {
            this.context = context;
        }

        //查询
        public List<Reply> GetRepliesByUserId(string id)
        {
            return context.Replies.Include("User").Include("Post")
                            .Where(m => m.User.UserId == id).ToList();
        }

        public Reply GetReplyByReplyId(int id)
        {
            return context.Replies.Include("User").Include("Post")
                            .Where(m => m.ReplyId == id).FirstOrDefault();
        }

        //增加
        public void AddReply(int PostId,string UserId,string content,DateTime dateTime)
        {
            Post Pres = context.Posts.Include("Replies").Where(m => m.PostId == PostId).FirstOrDefault() as Post;
            User Ures = context.Users.Include("AllReplys").Where(m => m.UserId == UserId).FirstOrDefault() as User;
            if(Pres == null || Ures == null) {
                return;
            }
            Reply r = new Reply { User = Ures, Post = Pres, Content = content, DateTime = dateTime };
            Ures.AllReplys.Add(r);
            Pres.Replies.Add(r);
            context.Replies.Add(r);
            context.SaveChanges();
        }

        //删除
        public void DeleteReply(int id)
        {
            Reply res = GetReplyByReplyId(id);
            if(res != null)
            {
                context.Replies.Remove(res);
                context.SaveChanges();
            }
        }
    }
}
