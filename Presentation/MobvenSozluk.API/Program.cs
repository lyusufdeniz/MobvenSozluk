using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobvenSozluk.API.Extensions;
using MobvenSozluk.API.Middlewares;
using MobvenSozluk.Caching;
using MobvenSozluk.Caching.Configurations;
using MobvenSozluk.Infrastructure.Mapping;
using MobvenSozluk.Infrastructure.Validations;
using MobvenSozluk.Persistance.Context;
using MobvenSozluk.Repository.Cache;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(UserDtoValidator).Assembly);


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddLoggingExtension();


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    });
});


var redisConfiguration = builder.Configuration.GetSection("ConnectionStrings").Get<RedisConfiguration>();
builder.Services.AddSingleton(redisConfiguration);

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ElasticLoggingMiddleware>();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.Run();
