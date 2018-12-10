using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mango.Framework.Services.Cache;
using Microsoft.Extensions.Caching.Redis;
namespace Mango.Manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddMvc(options => {
                options.Filters.Add(new Extensions.AuthorizationFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSession();
            //注册自定义服务
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions()
            {
                Configuration = Configuration.GetSection("Cache:ConnectionString").Value,
                InstanceName = Configuration.GetSection("Cache:InstanceName").Value
            }));
            //注册自定义服务
            Framework.Services.ServiceContext.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //启用会话存储(Session)
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //数据库连接字符串
            Framework.Core.Configuration.AddItem("ConnectionStrings", Configuration.GetSection("ConnectionStrings").Value);
        }
    }
}
