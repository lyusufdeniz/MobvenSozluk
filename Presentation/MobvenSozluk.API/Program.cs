using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobvenSozluk.API.Extensions;
using MobvenSozluk.API.Middlewares;
using MobvenSozluk.Domain.Abstract;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Mapping;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Infrastructure.Validations;
using MobvenSozluk.Persistance.Context;
using MobvenSozluk.Repository.Services;
using System.Reflection;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Repository.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddIdentityServices(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(UserDtoValidator).Assembly);
builder.Services.AddScoped(typeof(IPagingService<>), typeof(PagingService<>));
builder.Services.AddScoped(typeof(ISortingService<>), typeof(SortingService<>));
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
builder.Services.AddScoped(typeof(IEntryService), typeof(EntryService));
builder.Services.AddScoped(typeof(ITitleService), typeof(TitleService));
builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IRoleService), typeof(RoleService));
builder.Services.AddScoped(typeof(IFilteringService<>), typeof(FilteringService<>));



builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    });
});

//builder.Host.UseServiceProviderFactory
//    (new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterModule(new RepoServiceModules()));


IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
