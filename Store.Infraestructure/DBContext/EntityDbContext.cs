using Store.Infraestructure.Mapping;
using Store.Infraestructure.Resource;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Store.Infraestructure.DBContext
{
    public class EntityDbContext : DbContext
    {

        public EntityDbContext() : base(Implementation.StringKey)
        {
            Database.SetInitializer<EntityDbContext>(null);
        }

        public EntityDbContext(string connection) : base(Implementation.StringKey)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
        }

    }
}
