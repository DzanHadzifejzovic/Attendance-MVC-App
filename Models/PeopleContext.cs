using Microsoft.EntityFrameworkCore;

namespace FIsrtMVCapp.Models
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> People { get; set;}
        public PeopleContext()
        {
        }

        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer("Data Source=AUTOBVT-LV65O4Q\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
           // optionsBuilder.UseSqlServer(Program.Configuration.GetConnectionString("PrimaryConnectionString"));
        }
    }
}
