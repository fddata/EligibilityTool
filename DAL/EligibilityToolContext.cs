using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EligibilityTool.Models;

namespace EligibilityTool.DAL
{
    public class EligibilityToolContext : DbContext
    {

        public EligibilityToolContext() : base("EligibilityToolContext") 
        { 
        }

        public DbSet<Card> Cards { get; set; }

        public DbSet<CardApplication> CardApplications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}