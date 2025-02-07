using Microsoft.EntityFrameworkCore;
using Delbank.Infra.Data.SQL.Context;
using Delbank.Application.Injectors;
using Delbank.Application.Mappers;
using Delbank.Messaging.Consumers;
using Delbank.Domain.Entities.NoSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//SQL CONNECTION STRING
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<SqlContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

//NOSQL CONNECTION STRING


//Injections
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.RepositoriesInjectr();
builder.Services.ServicesInjecetor();
builder.Services.CommonsInjector();
builder.Services.MessagingInjector();

builder.Services.AddSingleton(AutMapperConfig.ConfigureAutoMapper());


builder.Services.Configure<DvdNoSQLEntitySettings>(builder.Configuration.GetSection("ConnectionStringNoSql"));


builder.Services.AddMemoryCache();


builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyHeader()
          .AllowAnyMethod()
          .SetIsOriginAllowed((host) => true);
}));



var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
