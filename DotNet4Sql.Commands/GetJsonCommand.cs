using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;


namespace DotNet4Sql.Commands
{
    [Cmdlet(VerbsCommon.Get, "json")]
    public class GetJsonCommand : Cmdlet, IContractResolver
    {
        [Parameter()]
        [ValidateNotNull]
        public string FromUrl { get; set; }
    
        [Parameter()]
        [ValidateNotNull]
        public string OfType { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            var pending_json = (new HttpClient()).GetStringAsync(this.FromUrl);
            pending_json.Wait();
            var json = pending_json.Result;
            var itemType = Type.GetType(this.OfType);
            var objects = JsonConvert.DeserializeObject<JObject[]>(json).Select(xx => xx.ToObject(itemType)).ToArray();
            Array.ForEach(objects, WriteObject);
        }

        JsonContract IContractResolver.ResolveContract(Type type)
        {
            throw new NotImplementedException();
        }
    }
}