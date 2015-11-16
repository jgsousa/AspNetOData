using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sousa.PricingEngine.OData.SAP
{
    public abstract class ODataProxy<baseclass, outputclass>
    {
        public abstract string baseURL { get; }
        public abstract string options { get; }
        private string _url;
        private AuthenticationHeaderValue _credentials;

        abstract public List<outputclass> Convert(D<baseclass> scheme);

        public ODataProxy(string URL, AuthenticationHeaderValue credentials)
        {
            _url = URL;
            _credentials = credentials;
        }

        public List<outputclass> GetResults()
        {
            var model = CallService();
            if (model != null)
            {
                return Convert(model);
            }
            else
            {
                return null;
            }
        }

        private D<baseclass> CallService()
        {
            var URL = _url + baseURL;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = _credentials;

            // List data response.
            HttpResponseMessage response = null;
            try
            {
                response = client.GetAsync(options).Result;  // Blocking call!
            }
            catch(AggregateException e)
            {
                var exc = e.InnerExceptions[0];
                throw new HttpRequestException(exc.InnerException.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    return JsonConvert.DeserializeObject<D<baseclass>>(jsonString.Result);
                }
                catch (AggregateException e)
                {
                    throw new HttpRequestException("JSON payload unparsable.");
                }
            }
            else
            {
                throw new HttpRequestException("Bad request");
            }
        }
    }
}
