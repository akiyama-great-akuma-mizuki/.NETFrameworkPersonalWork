using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Server.Entity;
public class ReportDAO : DbContext
{
    public ReportDAO(DbContextOptions<ReportDAO> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<Report> Reports { get; set; }
}