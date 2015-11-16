using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Sousa.PricingEngine.OData.SAP
{

    public class SAPPricingScheme
    {
        public Metadata __metadata { get; set; }
        public string SchemeId { get; set; }
        public string Description { get; set; }
        public Results<SAPPricingStep> Steps { get; set; }
    }

    public class SAPPricingStep
    {
        public Metadata __metadata { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }
        public string ConditionType { get; set; }
        public string Subtotal { get; set; }
        public int BaseFrom { get; set; }
        public int BaseTo { get; set; }
        public bool IsManual { get; set; }
    }

    public class SAPPricingOutput
    {
        public string SchemeId { get; set; }
        public string Description { get; set; }
        public List<SAPPricingStep> Steps { get; set; }
    }

    public class SAPPricingSchemeProxy : ODataProxy<SAPPricingScheme, SAPPricingOutput>
    {
        public SAPPricingSchemeProxy(string URL, AuthenticationHeaderValue credentials) : base(URL, credentials)
        {
        }

        public override string baseURL
        {
            get
            {
                return "/PricingSchemes";
            }
        }

        public override string options
        {
            get
            {
                return "?$format=json&$expand=Steps";
            }
        }

        public override List<SAPPricingOutput> Convert(D<SAPPricingScheme> scheme)
        {
            var outputArray = new List<SAPPricingOutput>();
            foreach (var item in scheme.d.results)
            {
                var output = new SAPPricingOutput();
                output.SchemeId = item.SchemeId;
                output.Description = item.Description;
                output.Steps = new List<SAPPricingStep>();
                foreach (var subitem in item.Steps.results)
                {
                    output.Steps.Add(subitem);
                }
                outputArray.Add(output);
            }
            return outputArray;
        }
    }
}
