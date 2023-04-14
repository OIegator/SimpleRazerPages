using SimpleRazerPages.Services;
using SimpleRazerPages.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SimpleRazerPages
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IDataReader, DataReader>();
            //builder.Services.AddSqlite<Context>("DataSource=test.squlite");
            //builder.Services.AddSqlite<Context>(
            //     builder.Configuration.GetConnectionString("Context"));
            // builder.Services.Add<IDataReader, IDataReader>();

            builder.Services.AddDbContext<Context>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<Context>();
                context.Database.EnsureCreated();
                // DbInitializer.Initialize(context);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}