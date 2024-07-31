using Microsoft.EntityFrameworkCore;
using TVM.Models;

namespace TVM.DbContexts
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }


        public DbSet<Data> Datas { get; set; }
    }
}
