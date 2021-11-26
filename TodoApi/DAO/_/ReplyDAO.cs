using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Server.Entity;
public class ReplyDAO : DbContext
{
    public ReplyDAO(DbContextOptions<ReplyDAO> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<Reply> Replies { get; set; }
}
