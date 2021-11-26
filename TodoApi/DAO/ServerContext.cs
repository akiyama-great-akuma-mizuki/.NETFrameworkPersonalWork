using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Server.Entity;

namespace Server.DAO
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> Tokens { get; set; }
    }
}