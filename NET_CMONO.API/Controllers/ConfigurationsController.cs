using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Model;
using NET_CMONO.Service;
using Microsoft.Extensions.Logging;

namespace NET_CMONO.API.Controllers
{
    /// <summary>
    /// 系统所有配置项
    /// </summary>
    [Route("api/[controller]")]
    public class ConfigurationsController : Controller
    {

        public ILogger<ConfigurationsController> logger{ get;set; }

        public ConfigurationService cfgService { get; set; }

        /// <summary>
        /// 获取系统全部配置项
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public IEnumerable<ConfigurationModel> Get()
        {
            logger.LogInformation("你访问了首页");
            return cfgService.GetConfigurationList("").Items;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
