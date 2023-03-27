using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobvenSozluk.API.Extensions;
using MobvenSozluk.API.Middlewares;
using MobvenSozluk.Infrastructure.Mapping;
using MobvenSozluk.Infrastructure.Validations;
using MobvenSozluk.Persistance.Context;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingExtension();
builder.Host.UseSerilog();

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


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    });
});

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

