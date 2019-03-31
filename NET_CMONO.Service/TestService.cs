using System;
using System.Collections.Generic;
using Autofac.Extras.DynamicProxy;
using NET_CMONO.Framework;
using NET_CMONO.Service.Interceptors;

namespace NET_CMONO.Service
{
    public interface ITestService : IDependency
    {
        Guid MyProperty { get; }
        List<string> GetList(string a);
    }

    [Intercept(typeof(AOPTestInterceptor))]
    public class TestService : ITestService
    {
        public TestService()
        {
            MyProperty = Guid.NewGuid();
        }
        public Guid MyProperty { get; set; }
        public List<string> GetList(string a)
        {
            return new List<string>() { "LiLei", "ZhangSan", "LiSi" };
        }


    }
}
