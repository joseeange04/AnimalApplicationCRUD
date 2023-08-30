using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies; 

namespace WebAnimalApplicationNET 
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                // Configuration d'autres options du cookie
                options.LoginPath = "/User/Login"; // Chemin vers la page de connexion
                options.LogoutPath = "/User/Logout"; // Chemin vers la page de déconnexion
                options.AccessDeniedPath = "/User/AccessDenied"; // Chemin vers la page d'accès refusé
                options.ReturnUrlParameter = "returnUrl"; // Paramètre pour la redirection après l'authentification
            });

            // Configurez d'autres services ici...

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configurez le gestionnaire d'erreurs en production ici...
            }

            app.UseAuthentication(); // Ajoutez cette ligne pour activer l'authentification

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
