using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Server.Entity;
public class AdminDAO : DbContext
{
    public AdminDAO(DbContextOptions<UserDAO> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<Admin> Admins { get; set; }
}