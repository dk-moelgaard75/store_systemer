using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kmr_uge2.Models
{
    public class CovidTestModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "ssn")]
        public string SocialSecurityNumber { get; set; } //Reference/foreign key to the person

        [JsonProperty(PropertyName = "dateoftest")]
        public string DateOfTest { get; set; }

        [JsonProperty(PropertyName = "ispositive")]
        public bool isPositive { get; set; } //a test being positiv indicates the person is infected with Covid-19

    }
}
