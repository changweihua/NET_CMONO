using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyModel;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Service;
using NET_CMONO.Service.Interceptors;

namespace NET_CMONO.MVC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // //所有程序集 和程序集下类型
            // var deps = DependencyContext.Default;
            // var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");//排除所有的系统程序集、Nuget下载包
            // var listAllType = new List<Type>();
            // foreach (var lib in libs)
            // {
            //     try
            //     {
            //         var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
            //         listAllType.AddRange(assembly.GetTypes().Where(type => type != null));
            //     }
            //     catch { }
            // }

            //注册所有实现了 IDependency 接口的类型
            Assembly[] assemblies = Directory.GetFiles(AppContext.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            Type baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Service"))
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().EnableClassInterceptors();//.InterceptedBy(typeof(AOPTestInterceptor));

            Type interceptorType = typeof(IInterceptor);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => interceptorType.IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Interceptor"))
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired();//.EnableClassInterceptors();//.InterceptedBy(typeof(AOPTestInterceptor));


            //属性注入
            // builder.RegisterType<TestService>().As<ITestService>().PropertiesAutowired().EnableInterfaceInterceptors();
        }
    }
}