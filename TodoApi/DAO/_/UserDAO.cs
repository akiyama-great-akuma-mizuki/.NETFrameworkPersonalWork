using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Server.Entity;
public class UserDAO : DbContext
{
    public UserDAO(DbContextOptions<UserDAO> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<User> Users { get; set; }
}