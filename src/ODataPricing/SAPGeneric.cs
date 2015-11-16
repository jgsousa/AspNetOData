using System.Collections.Generic;

namespace Sousa.PricingEngine.OData.SAP
{
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Deferred
    {
        public string uri { get; set; }
    }

    public class Results<type>
    {
        public List<type> results { get; set; }
    }

    public class D<type>
    {
        public Results<type> d { get; set; }
    }
}
