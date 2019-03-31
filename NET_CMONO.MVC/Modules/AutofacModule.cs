using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Service.Interceptors;

namespace NET_CMONO.MVC.Modules
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new AOPTestInterceptor());
            builder.RegisterType<IUnitOfWork>().AsImplementedInterfaces().PropertiesAutowired();

            //获取全部的Controller
            Assembly[] assemblies = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            var manager = new ApplicationPartManager();
            assemblies.ToList().ForEach(assembly =>
            {
                manager.ApplicationParts.Add(new AssemblyPart(assembly));
            });
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired().EnableClassInterceptors();//.InterceptedBy(typeof(ControllerInvokedInterceptor));

            //采用属性注入控制器
            // builder.RegisterType<Controllers.DTController>().PropertiesAutowired();
            // builder.RegisterType<Controllers.AutoDIController>().PropertiesAutowired();
            // builder.RegisterType<Controllers.HomeController>().PropertiesAutowired();
        }

        // protected override void AttachToComponentRegistration( IComponentRegistry registry,IComponentRegistration registration)
        // {
        //     registration.Activating += (s, e) =>
        //     {
        //         e.Context.InjectProperties(e.Instance);
        //     };
        // }
    }
}