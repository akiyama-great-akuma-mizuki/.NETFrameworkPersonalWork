using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Entity
{
    public class Post
    {
        public int PostId { get; set; }
        public User User { get; set; }
        
        public string Title { get; set; }
        public string Content { get; set; }

        public List<Reply> Replies { get; set; }
        public DateTime DateTime { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Post post &&
                   PostId == post.PostId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PostId);
        }
    }
}