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
        public DbSet<DllFiles> DllFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

        }
    }
}
