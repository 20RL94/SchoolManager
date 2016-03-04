using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSchoolManager
{
    public class SchoolContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public SchoolContext() : base("Name=SchoolDb")
        {
            //Database.SetInitializer(new SchoolContextInitializer());
        }

        private class SchoolContextInitializer : DropCreateDatabaseAlways<SchoolContext>
        {
            protected override void Seed(SchoolContext context)
            {
                base.Seed(context);
            }
        }
    }

    public class SchoolRepository<T> : EFRepository<T, SchoolContext> where T : class { }
}
