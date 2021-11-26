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
 * 
 */
namespace Server.Service
{
    public class ReportService : IService
    {
        public string ServiceName => nameof(ReportService);
        private ServerContext context;

        public ReportService(ServerContext context)
        {
            this.context = context;
        }

    }
}