using Ires2023Exam.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ires2023Exam.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ContainerEntity> Containers { get; set; }
    public DbSet<SpotEntity> Spots { get; set; }
}
