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

            // 加入 appsettings.json 配置
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // 讀取連接字串
            var connectionString = builder.Configuration.GetConnectionString("NorthwindDatabase");

            // 設定 DbContext 並使用 SQL Server
            builder.Services.AddDbContext<PubsContext>(options =>
                options.UseSqlServer(connectionString));

            // 加入服務和中介軟體配置
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
