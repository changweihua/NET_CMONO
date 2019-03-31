using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using NET_CMONO.EfRepository;
using NET_CMONO.Framework;
using NET_CMONO.MVC.Controllers;
using NET_CMONO.MVC.Exts;
using NET_CMONO.MVC.Modules;
using NET_CMONO.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;
using NET_CMONO.MVC.Middlewares;
using System.Globalization;
using System.Threading;
using NET_CMONO.MVC.Middlewares.Options;
using NET_CMONO.Model;
using NET_CMONO.MVC.Conventions;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace NET_CMONO.MVC
{
    public class Startup
    {
        // public Startup(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // var message = new MimeMessage();
                // message.From.Add(new MailboxAddress("AAAA", "dyelcwh@163.com"));
                // message.Subject = "本周工作报告";

                // TextPart body = new TextPart(TextFormat.Html)
                // {
                //     Text = "发一下本周工作报表 "
                // };

                // using (SmtpClient client = new SmtpClient())
                // {
                //     //Smtp服务器
                //     client.Connect("smtp.163.com", 25, false);
                //     //登录，发送
                //     //特别说明，对于服务器端的中文相应，Exception中有编码问题，显示乱码了
                //     client.Authenticate("注册的邮箱", "授权验证码");

                //     client.Send(message);
                //     //断开
                //     client.Disconnect(true);
                //     Console.WriteLine("发送邮件成功");
                // }
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        private IServiceCollection injectedServices;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //开启 Session
            //使用实现了IDistributedcache接口的服务来启用内存缓存。（例如使用内存缓存）
            services.AddDistributedMemoryCache();

            //添加redis
            // services.AddDistributedRedisCache(options =>
            // {
            //     options.Configuration = "localhost";
            // });

            //调用addsession方法
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10); //session活期时间
                options.Cookie.HttpOnly = true;//设为httponly
            });


            //开启 Session

            /// 自定义 CSRF 信息
            services.AddAntiforgery(option =>
            {
                option.Cookie.Name = "NET-CMONO-CSRF-COOKIE";
                option.FormFieldName = "NET_CMONO_HID_CSRF";
                option.HeaderName = "NET-CMONO-CSRF-HEADER";
            });

            //开启GZIP压缩
            services.AddResponseCompression();

            //开启本地缓存
            services.AddResponseCaching();

            services
                            .AddDbContext<PRMDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")))
                            //.AddDbContext<BloggingContext>(opt => opt.UseInMemoryDatabase("UnitOfWork"))
                            .AddUnitOfWork<PRMDbContext>();

            //替换控制器所有者
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddAutoMapper();

            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(options =>
            {
                // options.ValueProviderFactoties.Add(new JQueryQueryStringValueProviderFactory());
                options.Conventions.Add(new ArraryHandleQueryConvention());
                options.ModelBinderProviders.Insert(0, new SplitDateTimeModelBinderProvider());
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                //    options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //增加Cookie中间件配置
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "NET-CMONO-CookieAuthenticationScheme";
                options.DefaultChallengeScheme = "NET-CMONO-CookieAuthenticationScheme";
                options.DefaultSignInScheme = "NET-CMONO-CookieAuthenticationScheme";

            })
            .AddCookie("NET-CMONO-CookieAuthenticationScheme", options =>
            {
                //options.AccessDeniedPath = "/Account/Forbidden";
                options.LoginPath = "/NCM/Account/Login";
            });


            //日志查看，也是Diagnostics 中间件的一个功能。用起来也比较方便。
            // 需要额外添加一个 Microsoft.AspNetCore.Diagnostics.Elm 包。
            // 安装好包打开Startup.cs ,首先在 ConfigureServices 方法加入服务。
            services.AddElm(elmOptions =>
            {
                elmOptions.Filter = (loggerName, loglevel) => loglevel == LogLevel.Debug;
            });

            injectedServices = services;

            //DT Core 自带注入,仅支持构造函数注入
            // services.AddTransient<ITestService, TestService>();

            //Autofac 注入
            var containerBuilder = new ContainerBuilder();
            //模块化注入
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.RegisterModule<ServiceModule>();
            //属性注入控制器
            // containerBuilder.RegisterTypes().PropertiesAutowired();
            // containerBuilder.RegisterType<Controllers.HomeController>().PropertiesAutowired();
            containerBuilder.Populate(services);
            // containerBuilder.RegisterType<ConfigurationService>().As<IDependency>().PropertiesAutowired().EnableInterfaceInterceptors();
            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            AutoMappingConfig.RegisterMappings();

            //使用usesession回调（usesession需在useMvc()方法前调用）
            app.UseSession();

            app.UseCookiePolicy();

            // app.UseElmPage(); 为指定日志显示页，app.UseElmCapture(); 记录日志。

            // 运行程序访问 http://localhost:5000/Elm ，就可以查看一些信息记录。
            app.UseElmPage();
            app.UseElmCapture();

            loggerFactory.AddConsole();

            loggerFactory.AddNLog();    //添加NLog  
            env.ConfigureNLog("nlog.config");    //读取Nlog配置文件  

            var _logger = loggerFactory.CreateLogger("Services");
            _logger.LogInformation($"Total Services Registered: {injectedServices.Count}");
            foreach (var service in injectedServices)
            {
                _logger.LogInformation($"Service: {service.ServiceType.FullName}\n      Lifetime: {service.Lifetime}\n      Instance: {service.ImplementationType?.FullName}");
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (env.IsDevelopment())
            {
                app.Map("/allservices", builder => builder.Run(async context =>
               {
                   context.Response.ContentType = "text/html; charset=utf-8";
                   await context.Response.WriteAsync($"<h1>所有服务{injectedServices.Count}个</h1><table><thead><tr><th>类型</th><th>生命周期</th><th>Instance</th></tr></thead><tbody>");
                   foreach (var svc in injectedServices)
                   {
                       await context.Response.WriteAsync("<tr>");
                       await context.Response.WriteAsync($"<td>{svc.ServiceType.FullName}</td>");
                       await context.Response.WriteAsync($"<td>{svc.Lifetime}</td>");
                       await context.Response.WriteAsync($"<td>{svc.ImplementationType?.FullName}</td>");
                       await context.Response.WriteAsync("</tr>");
                   }
                   await context.Response.WriteAsync("</tbody></table>");
               }));
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRequestCulture(new RequestCultureOptions
            {
                DefaultCulture = new CultureInfo("zh-cn")
            });
            // app.Run(async (context) =>
            //     {
            //         await context.Response.WriteAsync($"Hello {CultureInfo.CurrentCulture.DisplayName}");
            //     });

            app.Use(async (context, next) =>
            {
                context.Session.SetObject("CurrentUser",
                    new UserModel { LoginName = "James", LoginPwd = "james@bond.com" });
                await next();
            });

            // app.Run(async (context) =>
            // {
            //     var user = context.Session.GetObject<UserModel>("CurrentUser");
            //     await context.Response.WriteAsync($"{user.LoginName}, {user.LoginPwd}");
            // });

            // app.UseWelcomePage();
            app.UseDatabaseErrorPage();
            // app.UseStatusCodePages();

            // app.UseStatusCodePages("text/plain", "Error, status code: {0} \r LineZero");
            // app.UseStatusCodePagesWithRedirects("~/errors/{0}"); // 相对根路径
            // app.UseStatusCodePagesWithRedirects("/base/errors/{0}"); // 绝对路径
            // app.UseStatusCodePagesWithReExecute("/error/http{0}");
            // //上面两者的区别一个是跳转，一个是执行。

            app.UseRequestIP();//使用中间件
            app.UseAuth();//使用中间件

            //开启GZIP压缩
            app.UseResponseCompression();

            //使用本地缓存
            app.UseResponseCaching();

            app.UseResponseTime();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseAuthentication();
            // app.UseCookieAuthentication();
            //             app.AddAuthentication().AddCookie(new CookieAuthenticationOptions
            //             {
            //                 AuthenticationScheme = "MyCookieMiddlewareInstance",
            //                 LoginPath = new PathString("/Account/Unauthorized/"),
            //                 AccessDeniedPath = new PathString("/Account/Forbidden/"),
            //                 AutomaticAuthenticate = true,
            //                 AutomaticChallenge = true
            //             });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: "NCMRoute",
                    areaName: "NCM",
                    template: "{area:exists}/{controller=SPA}/{action=Index}");
            });
        }
    }
}
