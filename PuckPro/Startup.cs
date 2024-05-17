using Microsoft.EntityFrameworkCore;
using PuckPro.Models;

namespace PuckPro;

public class Startup
{
    public Startup(IConfiguration config)
    {
        Configuration = config;
    }
    
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var connection = "Host=localhost;Port=5433;Database=puckdb;Username=admin;Password=admin";
        services.AddDbContext<PuckDBContext>(opt =>
            opt.UseNpgsql(connection));

        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, PuckDBContext database)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        database.Database.Migrate();
    }
}