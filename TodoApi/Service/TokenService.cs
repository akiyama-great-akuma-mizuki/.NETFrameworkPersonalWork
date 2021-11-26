using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Server.Entity;
using System.Linq;
using Server.DAO;
using System;
namespace Server.Service
{
    public class TokenService : IService
    {
        public string ServiceName => nameof(TokenService);
        private ServerContext context;

        public TokenService(ServerContext context)
        {
            this.context = context;
        }

        public string GenerateToken()
        {
            return "KASHDFLASOQPEIQPXCM";
        }
    }
}
