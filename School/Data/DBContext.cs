using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.Areas.Admin.Models;
using School.Areas.Admission.Models;

namespace School
{
    public class DBContext : DbContext
    {
        /// Admin Area ///
        public DbSet<SchoolModel> SchoolModels { get; set; }
        public DbSet<SessionYearModel> SessionYearModels { get; set; }
        public DbSet<BoardModel> BoardModels { get; set; }
        public DbSet<ClassModel> ClassModels { get; set; }
        public DbSet<SectionModel> SectionModels { get; set; }
        public DbSet<DesginationModel> DesginationModels { get; set; }

        public DbSet<QualificationModel> QualificationModels { get; set; }
        public DbSet<QualificationTransModel> QualificationTransModels { get; set; }

        public DbSet<SubjectModel> SubjectModels { get; set; }
        public DbSet<SubJectTransModel> SubJectTransModels { get; set; }

        public DbSet<TeacherModel> TeacherModels { get; set; }
        public DbSet<StaffModel> StaffModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        public DbSet<FeesHeadModel> FeesHeadModels { get; set; }
        public DbSet<FeesStructureModel> FeesStructureModels { get; set; }
        public DbSet<FeesStructureTransModel> FeesStructureTransModels { get; set; }
        public DbSet<FeesStructureTransTempModel> FeesStructureTransTempModels { get; set; }
        // helping 
        public DbSet<CountryModel> CountryModels { get; set; }
        public DbSet<StateModel> StateModels { get; set; }
        public DbSet<CityModel> CityModels { get; set; }

        /// Admission Area ///
        public DbSet<StudentModel> StudentModels { get; set; }
        public DbSet<AdmissionModel> AdmissionModels { get; set; }
        public DbSet<EducationTransModel> EducationTransModels { get; set; }
        public DbSet<ActivityTransModel> ActivityTransModels { get; set; }

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
