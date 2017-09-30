using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Text;
using System.Web;

namespace TestProject.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("AppDb") {}
        static ApplicationContext()
        {
            Database.SetInitializer(new AppDbInit());
        }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Auto> Autos { get; set; }
        public DbSet<Immovables> Immovables { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Log> Log { get; set; }
    }

    public class AppDbInit : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            List<string> paths = new List<string>
            {
                HttpContext.Current.Server.MapPath("~\\SQLScripts\\AutosTrigger.sql"),
                HttpContext.Current.Server.MapPath("~\\SQLScripts\\ImmovablesTrigger.sql"),
                HttpContext.Current.Server.MapPath("~\\SQLScripts\\PetsTrigger.sql"),
                HttpContext.Current.Server.MapPath("~\\SQLScripts\\PicturesTrigger.sql")
            };
            foreach(string path in paths)
            {
                string query = File.ReadAllText(path, Encoding.GetEncoding(1251));
                context.Database.ExecuteSqlCommand(query);
            }
            base.Seed(context);
        }
    }
}