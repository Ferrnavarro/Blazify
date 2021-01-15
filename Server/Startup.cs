using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using SpotifyAPI.Web;
using Blazify.Server.Services;
using System;
using static SpotifyAPI.Web.Scopes;
using System.Collections.Generic;

namespace Blazify.Server
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
            services.AddHttpContextAccessor();
            services.AddSingleton(SpotifyClientConfig.CreateDefault());
            services.AddScoped<SpotifyClientBuilder>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Spotify", policy =>
                {
                    policy.AuthenticationSchemes.Add("Spotify");
                    policy.RequireAuthenticatedUser();
                });
            });
            services
              .AddAuthentication(options =>
              {
                  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              })
              .AddCookie(options =>
              {
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
              })
              .AddSpotify(options =>
              {
                  options.ClientId = Configuration["SPOTIFY_CLIENT_ID"];
                  options.ClientSecret = Configuration["SPOTIFY_CLIENT_SECRET"];
                  options.CallbackPath = "/Auth/callback";
                  options.SaveTokens = true;

                  var scopes = new List<string> {
            UserReadEmail, UserReadPrivate, PlaylistReadPrivate, PlaylistReadCollaborative
                };
                  options.Scope.Add(string.Join(",", scopes));
              });
            services.AddRazorPages()
              .AddRazorPagesOptions(options =>
              {
                  options.Conventions.AuthorizeFolder("/", "Spotify");
              });

            /*
            services.AddHttpContextAccessor();
            services.AddSingleton(SpotifyClientConfig.CreateDefault());
            services.AddScoped<SpotifyClientBuilder>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Spotify", policy =>
                {
                    policy.AuthenticationSchemes.Add("Spotify");
                    policy.RequireAuthenticatedUser();
                });
            });
            services
              .AddAuthentication(options =>
              {
                  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              })
              .AddCookie(options =>
              {
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
              })
              .AddSpotify(options =>
              {
                  options.ClientId = Configuration["SPOTIFY_CLIENT_ID"];
                  options.ClientSecret = Configuration["SPOTIFY_CLIENT_SECRET"];
                  options.CallbackPath = "/Auth/callback";
                  options.SaveTokens = true;

                  var scopes = new List<string> {
            UserReadEmail, UserReadPrivate, PlaylistReadPrivate, PlaylistReadCollaborative
                };
                  options.Scope.Add(string.Join(",", scopes));
              });



            services.AddControllersWithViews();
            services.AddRazorPages()
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/", "Spotify");
            });*/
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
                // app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /* app.UseHttpsRedirection();
             app.UseBlazorFrameworkFiles();
             app.UseStaticFiles();

             app.UseRouting();

             app.UseAuthentication();
             app.UseAuthorization();

             app.UseEndpoints(endpoints =>
             {
                 endpoints.MapRazorPages();
                 endpoints.MapControllers();
                 endpoints.MapFallbackToFile("index.html");
             });*/


        }
    }
}
