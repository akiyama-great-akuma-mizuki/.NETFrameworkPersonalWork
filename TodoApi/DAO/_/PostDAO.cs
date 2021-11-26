using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Server.Entity;
public class PostDAO : DbContext
{
    public PostDAO(DbContextOptions<PostDAO> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<Post> Posts { get; set; }
}