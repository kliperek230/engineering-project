using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;


//namespace foodmateapp
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddCors();
//            services.AddControllers();
//            services.AddControllersWithViews();
//            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//            services.AddDistributedMemoryCache();

//            var appSettingsSection = Configuration.GetSection("AppSettings");
//            services.Configure<AppSettings>(appSettingsSection);

//            var appSettings = appSettingsSection.Get<AppSettings>();
//            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
//            services.AddAuthentication(x =>
//            {
//                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(x =>
//            {
//                x.RequireHttpsMetadata = false;
//                x.SaveToken = true;
//                x.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false
//                };
//            });

//            services.AddScoped<IUserService, UserService>();
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseRouting();

//            // global cors policy
//            app.UseCors(x => x
//                .AllowAnyOrigin()
//                .AllowAnyMethod()
//                .AllowAnyHeader());

//            app.UseAuthentication();
//            app.UseAuthorization();

//            app.UseEndpoints(endpoints => {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}

namespace foodmateapp
//{                    POPRZEDNIE STARTUP
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddControllersWithViews();
//            services.AddSession();
//            services.AddMvc();
//            services.AddControllers();
//            services.AddRazorPages();

//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {

//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            app.UseSession();

//            app.UseMvc();

//            app.UseStaticFiles();
//            app.UseRouting();

//        }
//    }
//}

{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddMvc(option => option.EnableEndpointRouting = false);
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseMvc();
        }
    }
}
