using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.Areas.Admin.Models;

namespace School
{
    public class DBContext : DbContext
    {
        // Project Flow 
        public DbSet<TeacherModel> TeacherModels { get; set; }
        public DbSet<BoardModel> BoardModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source = (localdb)\ProjectsV13; Initial Catalog = Rohit; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

    }
}
