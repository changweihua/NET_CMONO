using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NET_CMONO.Service;
using NET_CMONO.Service.Interceptors;

namespace NET_CMONO.MVC.Controllers
{
    [Intercept(typeof(ControllerInvokedInterceptor))]
    public class DTController : Controller
    {
        public ILogger logger { get; set; }
        public ILoggerFactory loggerFactory { get; set; }
        public ConfigurationService cfgService { get; set; }
        public ITestService testService { get; set; }
    }
}