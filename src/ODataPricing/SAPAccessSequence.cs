using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Sousa.PricingEngine.OData.SAP
{

    public class SAPAccessStep
    {
        public Metadata __metadata { get; set; }
        public int StepNumber { get; set; }
        public string TableNumber { get; set; }
        public bool Exclusion { get; set; }
    }

    public class SAPAccessSequence
    {
        public Metadata __metadata { get; set; }
        public string AccessSequenceId { get; set; }
        public string Description { get; set; }
        public Results<SAPAccessStep> Steps { get; set; }
    }

    public class SAPAccessOutput
    {
        public string AccessSequenceId { get; set; }
        public string Description { get; set; }
        public List<SAPAccessStep> Steps { get; set; }
    }

    public class SAPAccessProxy : ODataProxy<SAPAccessSequence, SAPAccessOutput>
    {
        public override string baseURL
        {
            get
            {
                return "/AccessSequences";
            }
        }

        public override string options
        {
            get
            {
                return "?$format=json&expand=Steps";
            }
        }

        public SAPAccessProxy(string URL, AuthenticationHeaderValue credentials) : base(URL, credentials)
        {

        }

        public override List<SAPAccessOutput> Convert(D<SAPAccessSequence> scheme)
        {
            var outputArray = new List<SAPAccessOutput>();
            var access = new SAPAccessOutput();
            foreach (var item in scheme.d.results)
            {
                access.AccessSequenceId = item.AccessSequenceId;
                access.Description = item.Description;
                access.Steps = new List<SAPAccessStep>();
                foreach (var step in item.Steps.results)
                {
                    access.Steps.Add(step);
                }
                outputArray.Add(access);
            }
            return outputArray;
        }
    }
}
