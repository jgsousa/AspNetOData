using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Sousa.PricingEngine.OData.SAP
{
    public class SAPConditionType
    {
        public Metadata __metadata { get; set; }
        public string ConditionTypeID { get; set; }
        public string Description { get; set; }
        public string CalculationType { get; set; }
        public string AccessSequence { get; set; }
    }

    public class SAPConditionTypeProxy : ODataProxy<SAPConditionType, SAPConditionType>
    {
        public override string baseURL
        {
            get
            {
                return "/ConditionTypes";
            }
        }

        public override string options
        {
            get
            {
                return "?$format=json";
            }
        }

        public SAPConditionTypeProxy (string URL, AuthenticationHeaderValue credentials) : base(URL, credentials){

        }

        public override List<SAPConditionType> Convert(D<SAPConditionType> scheme)
        {
            var outputArray = new List<SAPConditionType>();
            foreach (var item in scheme.d.results)
            {
                outputArray.Add(item);
            }
            return outputArray;
        }
    }
}
