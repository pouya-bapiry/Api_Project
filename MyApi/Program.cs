using Data;
using Microsoft.EntityFrameworkCore;
using MyApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region DatabaseConfig
var connectionString = builder.Configuration.GetConnectionString("MyApi");

builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(connectionString), ServiceLifetime.Transient);
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();


startup.Configure(app, app.Environment);

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
