using System;
using System.Collections;
using System.Collections.Generic;
namespace Server.Entity
{
    public class User
    {
        public String UserId { get; set; }
        public String UserPassword { get; set; }
        public string UserName { get; set; }
        public List<Post> AllPosts { get; set; }
        public List<Reply> AllReplys { get; set; }

        public int Level { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   UserId == user.UserId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId);
        }
    }
}