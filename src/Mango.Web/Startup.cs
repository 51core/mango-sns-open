using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Mango.Framework.Services.Cache;
using Microsoft.Extensions.Caching.Redis;
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
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //});
            //���SignalR
            services.AddSignalR();

            services.AddSession();
            services.AddMemoryCache();

            services.AddControllersWithViews(options => {
                options.Filters.Add(new Extensions.AuthorizationActionFilter());
            }).AddNewtonsoftJson();
            services.AddRazorPages();
            //
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            //���Session��������
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //ע���Զ������
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions()
            {
                Configuration = Configuration.GetSection("Cache:ConnectionString").Value,
                InstanceName = Configuration.GetSection("Cache:InstanceName").Value
            }));
            Framework.Services.ServiceContext.RegisterServices(services.BuildServiceProvider());
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //���ûỰ�洢(Session)
            app.UseSession();
            
            app.UseCookiePolicy();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //����SignalR
                endpoints.MapHub<Extensions.MessageHub>("/MessageHub");
                //
                endpoints.MapAreaControllerRoute(
                   name: "area",
                   areaName: "User",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });

            //���ݿ������ַ���
            Framework.Core.Configuration.AddItem("ConnectionStrings", Configuration.GetSection("ConnectionStrings").Value);
            //������������
            Framework.Core.Configuration.AddItem("Upyun_BucketName", Configuration.GetSection("Upyun:BucketName").Value);
            Framework.Core.Configuration.AddItem("Upyun_BucketPassword", Configuration.GetSection("Upyun:BucketPassword").Value);
            //�����ƶ�������
            Framework.Core.Configuration.AddItem("Aliyun_AccessKeyId", Configuration.GetSection("Aliyun:AccessKeyId").Value);
            Framework.Core.Configuration.AddItem("Aliyun_AccessKeySecret", Configuration.GetSection("Aliyun:AccessKeySecret").Value);
            Framework.Core.Configuration.AddItem("Aliyun_SmsSignature", Configuration.GetSection("Aliyun:SmsSignature").Value);
            Framework.Core.Configuration.AddItem("Aliyun_SmsTempletKey", Configuration.GetSection("Aliyun:SmsTempletKey").Value);
            //��Ѷ������
            Framework.Core.Configuration.AddItem("Tencent_VerificationAppId", Configuration.GetSection("Tencent:VerificationAppId").Value);
            Framework.Core.Configuration.AddItem("Tencent_VerificationAppSecretKey", Configuration.GetSection("Tencent:VerificationAppSecretKey").Value);
        }
    }
}
