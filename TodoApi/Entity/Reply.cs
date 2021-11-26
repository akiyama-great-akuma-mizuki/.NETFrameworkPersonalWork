using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Entity
{
   public class Reply
    {
        public int ReplyId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public String Content { get; set; }
        public DateTime DateTime { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Reply reply &&
                   ReplyId == reply.ReplyId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ReplyId);
        }
    }

}