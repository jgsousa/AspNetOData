using System.Net.Http.Headers;
using Microsoft.Framework.Configuration;
using Sousa.PricingEngine.OData.SAP;
using System;
using System.Net.Http;

namespace RestTest
{
    public class Program
    {
        public IConfiguration Configuration { get; set; }

        public void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json");
            Configuration = builder.Build();

            var serviceURL = Configuration["baseURL"];
            var proxy = new SAPPricingSchemeProxy(serviceURL, new AuthenticationHeaderValue("Basic", "ZGVsb2l0dGU6c2FwMTIz"));

            try
            {
                var result = proxy.GetResults();
                foreach (var item in result)
                {
                    Console.WriteLine("Esquema: " + item.SchemeId + " - " + item.Description);
                }
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
