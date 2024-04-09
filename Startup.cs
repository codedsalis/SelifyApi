using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SelifyApi.Data;
using SelifyApi.Interfaces;
using SelifyApi.Services;

namespace SelifyApi;
public class Startup
{
    public IConfiguration Configuration{ get; set; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options => 
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IFoodService, FoodService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // if (app.Environment.IsDevelopment())
        // {
            app.UseSwagger();
            app.UseSwaggerUI();
        // }

        // app.UseHttpsRedirection();


        // app.MapControllers();
    }
}
