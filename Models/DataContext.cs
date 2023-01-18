using Microsoft.EntityFrameworkCore;

namespace Juan.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
    }
}
