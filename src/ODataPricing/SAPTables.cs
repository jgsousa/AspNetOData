using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Sousa.PricingEngine.OData.SAP
{

    public class SAPTableFields
    {
        public Metadata __metadata { get; set; }
        public string FieldName{ get; set; }
        public string FieldType { get; set; }
    }

    public class SAPTable
    {
        public Metadata __metadata { get; set; }
        public string TableNumber { get; set; }
        public Results<SAPTableFields> Fields { get; set; }
    }

    public class SAPTableOutput
    {
        public string TableNumber { get; set; }
        public List<SAPTableFields> Fields { get; set; }
    }

    public class SAPTableProxy : ODataProxy<SAPTable, SAPTableOutput>
    {
        public override string baseURL
        {
            get
            {
                return "/Tables";
            }
        }

        public override string options
        {
            get
            {
                return "?$format=json&$expand=Fields";
            }
        }

        public SAPTableProxy(string URL, AuthenticationHeaderValue credentials) : base(URL, credentials)
        {

        }

        public override List<SAPTableOutput> Convert(D<SAPTable> scheme)
        {
            var outputArray = new List<SAPTableOutput>();
            var table = new SAPTableOutput();
            foreach (var item in scheme.d.results)
            {
                table.TableNumber = item.TableNumber;
                table.Fields = new List<SAPTableFields>();
                foreach (var field in item.Fields.results)
                {
                    table.Fields.Add(field);
                }
                outputArray.Add(table);
            }
            return outputArray;
        }
    }
}
