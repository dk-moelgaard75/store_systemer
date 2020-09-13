using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kmr_uge2.Models
{
    public class PersonModel
    {
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName ="firstname")]
        public string FirstName { get; set; }
        
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "ssn")]
        public string SocialSecurityNumber { get; set; }

        public string NameIdentifier 
        { 
            get
            {
                return FirstName + " - " + LastName + " (" + SocialSecurityNumber.Substring(0, 8) + ")";
            }
        }
    }
}
