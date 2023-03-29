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
using Serilog.Sinks.Elasticsearch;
using Serilog.Exceptions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();


builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>());

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
