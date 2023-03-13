using Autofac;
using MobvenSozluk.Infrastructure.Mapping;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Persistance.Context;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Persistance.UnitOfWorks;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System.Reflection;
using Module = Autofac.Module;

namespace MobvenSozluk.API.Modules
{
    public class RepoServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith
            ("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope(); // addScoped ile aynı method.

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith
            ("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

           
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            //var serviceAssembly = Assembly.GetAssembly(typeof(Service<>));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
