using Autofac;
using Autofac.Integration.WebApi;
using Store.Application.Services.Service;
using Store.Infraestructure;
using Store.Infraestructure.DBContext;
using Store.Infraestructure.Repository;
using Store.Repository;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace Store.WebAPI.App_Start
{
    public class DependencyInjection
    {
        public static IContainer Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(typeof(ProductService).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ProductRepository).GetTypeInfo().Assembly).AsImplementedInterfaces();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<EntityDbContext>().As<DbContext>().InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}