using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NET_CMONO.API.SwaggerExt
{
    /// <summary>
    /// 添加通用参数，若in='header'则添加到header中,默认query参数
    /// </summary>
    public class AssignOperationVendorExtensions : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();
            //MemberAuthorizeAttribute 自定义的身份验证特性标记
            // var isAuthor = operation != null && context != null && context.ApiDescription.ControllerAttributes().Any(e => e.GetType() == typeof(MemberAuthorizeAttribute)) || context.ApiDescription.ActionAttributes().Any(e => e.GetType() == typeof(MemberAuthorizeAttribute));
            // if (isAuthor)
            // {
            //     //in query header 
            //     operation.Parameters.Add(new NonBodyParameter() { 
            //             Name = "x-token", 
            //             In = "header", //query formData ..
            //             Description = "身份验证票据", 
            //             Required = false, 
            //             Type = "string" 
            //     });
            // }
        }
    }
}