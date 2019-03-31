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
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Service;
using NET_CMONO.API.Exts;
using Swashbuckle.AspNetCore.Swagger;
using NET_CMONO.API.SwaggerExt;

namespace NET_CMONO.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services
                .AddDbContext<PRMDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")))
                //.AddDbContext<BloggingContext>(opt => opt.UseInMemoryDatabase("UnitOfWork"))
                .AddUnitOfWork<PRMDbContext>();

            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddMvc();

            #region Inject Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Softtek_PRM API", Version = "v1", Description = "ABC",TermsOfService = "None",
                    Contact = new Contact { Name = "Shayne Boyer", Email = "", Url = "http://twitter.com/spboyer"},
                    License = new License { Name = "Use under LICX", Url = "http://url.com" } 
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Softtek_PRM.API.XML");
                c.IncludeXmlComments(xmlPath);
                //添加自定义参数，可通过一些特性标记去判断是否添加
                c.OperationFilter<AssignOperationVendorExtensions>();
                //添加对控制器的标签(描述)
                c.DocumentFilter<ApplyTagDescriptions>();

                // var _Project_Name ="Softtek_PRM.API";
                // typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                //     {
                //         c.SwaggerDoc(version, new Swashbuckle.AspNetCore.Swagger.Info
                //         {
                //             Version = version,
                //             Title = $"{_Project_Name} 接口文档",
                //             Description = $"{_Project_Name} HTTP API " + version,
                //             TermsOfService = "None"
                //         });
                //     });
                // var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                // var xmlPath = System.IO.Path.Combine(basePath, $"{_Project_Name}.xml");
                // c.IncludeXmlComments(xmlPath);
                // //添加自定义参数，可通过一些特性标记去判断是否添加
                // c.OperationFilter<AssignOperationVendorExtensions>();
                // //添加对控制器的标签(描述)
                // c.DocumentFilter<ApplyTagDescriptions>();
            });

            #endregion 
            
            #region Autofac

            var builder = new ContainerBuilder();

            builder.RegisterType<IUnitOfWork>().AsImplementedInterfaces().PropertiesAutowired();

            //注册所有实现了 IDependency 接口的类型
            Assembly[] assemblies = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            var manager = new ApplicationPartManager();
            assemblies.ToList().ForEach(assembly =>
            {
                manager.ApplicationParts.Add(new AssemblyPart(assembly));
            });
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();//.EnableClassInterceptors();//.InterceptedBy(typeof(ControllerInvokedInterceptor));


            Type baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Service"))
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired();

            // Type interceptorType = typeof(IInterceptor);
            // builder.RegisterAssemblyTypes(assemblies)
            //        .Where(type => interceptorType.IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Interceptor"))
            //        .AsSelf().AsImplementedInterfaces()
            //        .PropertiesAutowired();

            builder.Populate(services);

            var Container = builder.Build();
            
            return new AutofacServiceProvider(Container);

            #endregion 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //添加NLog
            loggerFactory.AddNLog();
            //引入Nlog配置文件
            env.ConfigureNLog("nlog.config");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();


            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = "swagger/ui";
                c.SwaggerEndpoint("v1/swagger.json", "Softtek_PRM.API");
            });

        }
    }
}
