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
 * UserService
 * 查询：1.按id查询，返回用户信息
 *      2.按UserName查询，返回所有同名用户
 * 修改：1.支持修改对应id用户的密码字段
 *      2.修改其他信息字段
 * 删除：1.根据id删除用户（保留其post和reply
 * 增加：1.根据用户信息新增用户，并分配一个id
 * 
 */
namespace Server.Service{


    public class UserService : IService
    {
        private ServerContext context;

        public string ServiceName => nameof(UserService);

        public UserService(ServerContext context)
        {
            this.context = context;
        }
        
        //根据id查询
        public   User GetUserById(string id)
        {
            var res = context.Users.Where(u => u.UserId == id);
            return res.FirstOrDefault();
        }
        //根据name查询
        public   List<User> GetUserByUserName(string name)
        {
            var res = context.Users.Where(u => u.UserName == name);
            return res.ToList();
        }           

        //修改密码字段
        public   void SetUserPassword(string id,string newPwd)
        {
            var res = context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if(res != null)
            {
                res.UserPassword = newPwd;
                context.SaveChanges();
            }
        }

        //修改其他信息
        public   void SetUserInfo(string id,string name)
        {
            var res = context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if(res != null)
            {
                res.UserName = name;
                context.SaveChanges();
            }
           
        }

        //删除用户
        public   void RemoveUserById(string id)
        {
            var res = context.Users.Where(m => m.UserId == id).FirstOrDefault();
            if(res != null)
            {
                res.AllPosts.ForEach(m => m.User = DeletedUser.GetInstance());
                res.AllReplys.ForEach(m => m.User = DeletedUser.GetInstance());
                context.Remove(res);
                context.SaveChanges();
            }
            
        }

        //增加用户
        public  bool AddUser(string name,string password)
        {
            var ran = new Random();
            int id = 0;
            bool norepeat = false;
            var res = context.Users.Where(m => m.UserId == name).FirstOrDefault();
            if(res != null)
            {
                return false;
            }
   
            //while (!norepeat)
            //{
            //    id = ran.Next(0, 999999);
            //    foreach(var m in res)
            //    {
            //        if (m.UserId == id)
            //        {
            //            norepeat = false;
            //            break;
            //        }
            //        norepeat = true;
            //    }

            //}
            User user = new User()
            {
                UserId = name,
                UserName = name,
                UserPassword = password,
                AllPosts = new List<Post>(),
                AllReplys = new List<Reply>()
                
            };
            context.Add(user);
            context.SaveChanges();
            return true;
        }



    }
}