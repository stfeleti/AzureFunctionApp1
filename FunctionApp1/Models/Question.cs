using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OurProject.Model
{
    public class Question
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString("n");

        [JsonProperty(PropertyName = "CodeStub")]
        public string CodeStub { get; set; }
        [JsonProperty(PropertyName = "Language")]
        public string Language { get; set; }
       
    }
}
