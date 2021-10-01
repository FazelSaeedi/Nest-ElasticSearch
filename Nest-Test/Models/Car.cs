using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nest_Test.Models
{

    public class Car
    {
   
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("model")]
        public int Model{ get; set; }
        [JsonPropertyName("colore")]
        public string Colore { get; set; }
        [JsonPropertyName("create")]
        public DateTime CreateAt { get; set; }


    }
    
}
