using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace NET_CMONO.MVC.Conventions
{
    /// <summary>
    /// 《WebAPI Get请求参数传入输入带有[]不识别问题》
    /// 1.https://localhost:44390/api/values?status=1&status=2
    /// 2.https://localhost:44390/api/values?status[]=1&status[]=2
    /// 3.https://localhost:44390/api/values?status[0]=1&status[1]=2
    /// </summary>
    public class ArraryHandleQueryConvention: IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            //parameter.ParameterType.IsArray || 
            if (parameter.Attributes.OfType<FromQueryAttribute>().Any())
                parameter.Action.Filters.Add(new ArrayQueryStringAttribute(parameter.ParameterName));
        }
    }
    public class ArrayQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly string _key;
        private readonly IQueryCollection _values;

        public ArrayQueryStringValueProvider(IQueryCollection values)
            : this(null, values)
        {
        }

        public ArrayQueryStringValueProvider(string key, IQueryCollection values)
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _values = values;
        }

        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key + "[]");

            if (_key != null && _key != key)
            {
                return result;
            }
            if (result != ValueProviderResult.None)
            {
                var splitValues = new StringValues(result.Values.ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }
            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ArrayQueryStringAttribute : Attribute, IResourceFilter
    {
        private readonly ArrayQueryStringValueProviderFactory _factory;

        public ArrayQueryStringAttribute(string key)
        {
            _factory = new ArrayQueryStringValueProviderFactory();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, _factory);
        }
    }
    public class ArrayQueryStringValueProviderFactory : IValueProviderFactory
    {
        private readonly string _key;

        public ArrayQueryStringValueProviderFactory() 
        {
        }

        public ArrayQueryStringValueProviderFactory(string key)
        {
            _key = key;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new ArrayQueryStringValueProvider(_key, context.ActionContext.HttpContext.Request.Query));
            return Task.CompletedTask;
        }
    }
}