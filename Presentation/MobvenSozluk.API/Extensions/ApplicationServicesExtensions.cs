using MobvenSozluk.Caching;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Persistance.UnitOfWorks;
using MobvenSozluk.Repository.Cache;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;

namespace MobvenSozluk.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITitleService, TitleService>();
            services.AddScoped<IEntryService, EntryService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IService<,>), typeof(Service<,>));
            services.AddScoped(typeof(IPagingService<>), typeof(PagingService<>));
            services.AddScoped(typeof(ISortingService<>), typeof(SortingService<>));
            services.AddScoped(typeof(IFilteringService<>), typeof(FilteringService<>));
            services.AddScoped(typeof(ISearchingService<>), typeof(SearchingService<>));
            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));
            #endregion

            return services;

        }
    }
}
