using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Entity
{
    public class Report
    {
        public int ReportId { get; set; }
        public bool isReply { get; set; }
        public Post Post { get; set; }
        public Reply Reply { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Report report &&
                   ReportId == report.ReportId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ReportId);
        }
    }
}