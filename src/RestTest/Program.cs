using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.Framework.Configuration;
using Sousa.PricingEngine.OData.SAP;

namespace RestTest
{
    public class D
    {
        public List<string> EntitySets { get; set; }
    }

    public class RootObject
    {
        public D d { get; set; }
    }

    public class Program
    {
        public IConfiguration Configuration { get; set; }

        public void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json");
            Configuration = builder.Build();

            var serviceURL = Configuration["baseURL"];
            var proxy2 = new SAPPricingSchemeProxy(serviceURL, new AuthenticationHeaderValue("Basic", "ZGVsb2l0dGU6c2FwMTIz"));
            var result2 = proxy2.GetResults();
        }
    }
}
