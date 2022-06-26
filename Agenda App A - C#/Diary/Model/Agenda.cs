using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Diary.Model
{
    public class Agenda 
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public int Telefone { get; set; }
        public int Idade { get; set; } 
        public String Cep { get; set; }
        public String Logradouro { get; set; }
        public String Complemento { get; set; }
        public String Localidade { get; set; }
        public string Uf { get; set; }
    }
}
