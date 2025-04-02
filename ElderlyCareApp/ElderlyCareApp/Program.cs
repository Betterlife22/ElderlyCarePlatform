using DAL;
using BLL;
using ElderlyCareApp.SignalRHub;

namespace ElderlyCareApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddBLL();
            builder.Services.AddSession();
            builder.Services.AddDAL(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapHub<HubSignalR>("/signalRHub");

            app.MapRazorPages();

            app.Run();
        }
    }
}
