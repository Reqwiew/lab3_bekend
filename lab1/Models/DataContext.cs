using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace lab1.Models;

    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Entrance> entrances => Set<Entrance>();
        public DbSet<Tariffs> Tariffses => Set<Tariffs>();
        public DbSet<Person> Users => Set<Person>();
}
