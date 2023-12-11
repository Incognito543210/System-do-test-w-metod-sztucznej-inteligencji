using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ParamInfo> ParamaInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

        }
    }
}
