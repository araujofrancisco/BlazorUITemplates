using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using MudBlazor.Services;
using FD.SampleData.Data;
using FD.SampleData.Contexts;
using FD.SampleData.Interfaces;
using MudBlazorUI.Services;
using System;

namespace MudBlazorUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            // setup dbcontextfactory and includes a scoped service to indicate the interface
            services.AddDbContextFactory<UserDbContext>(options =>
            {
#if DEBUG
                options.EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
#endif
            });
            services
                .AddSingleton<IDbContextFactory<UserDbContext>, DbContextFactory<UserDbContext>>()
                .AddScoped(p => p.GetRequiredService<IDbContextFactory<UserDbContext>>().CreateContext())
                .AddScoped<IDataService, DataService>();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            // custom seeder
            SeedDb(app.ApplicationServices);
        }

        // on generated data seed size will indicate how many records we want to create
        private const int seedSize = 1000;

        /// <summary>
        /// Executes database seeder just after all application services has started.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void SeedDb(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            DbInitializer<UserDbContext>.Initialize(context, seedSize);
        }

    }
}
