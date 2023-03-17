﻿using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Persistance.Repositories;
using MobvenSozluk.Persistance.UnitOfWorks;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;

namespace MobvenSozluk.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            #region CODE EXPLANATION SECTION
            /*
             The whole DI(Dependency Injection) methods stored here for the both single responsibility principle and clean coding principle.
             */
            #endregion

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<ITitleService, TitleService>();

            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryService, EntryService>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IService<,>), typeof(Service<,>));

            services.AddScoped(typeof(IPagingService<>), typeof(PagingService<>));
            services.AddScoped(typeof(ISortingService<>), typeof(SortingService<>));
            services.AddScoped(typeof(IFilteringService<>), typeof(FilteringService<>));
            services.AddScoped(typeof(ISearchingService<>), typeof(SearchingService<>));

            return services;

        }
    }
}