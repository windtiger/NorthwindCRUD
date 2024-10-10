using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using NorthwindCRUD.Models;

namespace NorthwindCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // �[�J appsettings.json �t�m
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Ū���s���r��
            var connectionString = builder.Configuration.GetConnectionString("NorthwindDatabase");

            // �]�w DbContext �èϥ� SQL Server
            builder.Services.AddDbContext<PubsContext>(options =>
                options.UseSqlServer(connectionString));

            // �[�J�A�ȩM�����n��t�m
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

       
    }
}
