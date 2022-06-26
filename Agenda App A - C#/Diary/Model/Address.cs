using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Diary.Model
{
    public class Address 
    {
        [System.Text.Json.Serialization.JsonPropertyName("cep")]
        public String Cep { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("logradouro")]
        public String Logradouro { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("complemento")]
        public String Complemento { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("localidade")]
        public String Localidade { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("uf")]
        public String Uf { get; set; }
    }
}