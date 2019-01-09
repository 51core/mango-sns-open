using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Mango.Framework.Services.Cache;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.AspNetCore.SignalR;
namespace Mango.Web
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
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            //添加SignalR
            services.AddSignalR();

            services.AddMvc(options => {
                options.Filters.Add(new Extensions.AuthorizationActionFilter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSession();
            services.AddMemoryCache();
            //
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //添加Session访问容器
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //注册自定义服务
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions()
            {
                Configuration = Configuration.GetSection("Cache:ConnectionString").Value,
                InstanceName = Configuration.GetSection("Cache:InstanceName").Value
            }));
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
                app.UseExceptionHandler("/home/error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //启用会话存储(Session)
            app.UseSession();
            //启用Signalr
            app.UseSignalR(routes =>
            {
                routes.MapHub<Extensions.MessageHub>("/MessageHub");
            });
            //
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Posts}/{action=Index}/{id?}");
            });

            //数据库连接字符串
            Framework.Core.Configuration.AddItem("ConnectionStrings", Configuration.GetSection("ConnectionStrings").Value);
            //又拍云配置项
            Framework.Core.Configuration.AddItem("Upyun_BucketName", Configuration.GetSection("Upyun:BucketName").Value);
            Framework.Core.Configuration.AddItem("Upyun_BucketPassword", Configuration.GetSection("Upyun:BucketPassword").Value);
            //阿里云短信配置
            Framework.Core.Configuration.AddItem("Aliyun_AccessKeyId", Configuration.GetSection("Aliyun:AccessKeyId").Value);
            Framework.Core.Configuration.AddItem("Aliyun_AccessKeySecret", Configuration.GetSection("Aliyun:AccessKeySecret").Value);
            Framework.Core.Configuration.AddItem("Aliyun_SmsSignature",Configuration.GetSection("Aliyun:SmsSignature").Value);
            Framework.Core.Configuration.AddItem("Aliyun_SmsTempletKey", Configuration.GetSection("Aliyun:SmsTempletKey").Value);
            //腾讯配置项
            Framework.Core.Configuration.AddItem("Tencent_VerificationAppId", Configuration.GetSection("Tencent:VerificationAppId").Value);
            Framework.Core.Configuration.AddItem("Tencent_VerificationAppSecretKey", Configuration.GetSection("Tencent:VerificationAppSecretKey").Value);
        }
    }
}
