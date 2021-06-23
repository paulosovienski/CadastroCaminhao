using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Volvo.Models
{
    //Classe que mapeia o DB utilizada na Migrations
    public class Caminhao
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Modelo { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]        
        public int AnoModelo { get; set; }
        public string Chassi { get; set; }
        public List<Caminhao> Caminhoes { get; set; }
    }

    //Classe que serve de modelo para o Dropdwnlist Modelos que vai para View
    //Teste verifica se o tipo retornado é correto
    public class ModeloCaminhao
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        
        public List<ModeloCaminhao> GetModelos()
        {
            List<ModeloCaminhao> lm = new List<ModeloCaminhao>() 
            {
                new ModeloCaminhao() { Id = 1, Modelo = "FH" },
                new ModeloCaminhao() { Id = 2, Modelo = "FM" }
            };            
           
            return (lm);
        }
    
    }

    //Classe que serve de modelo para o Dropdwnlist AnoModelo que vai para View
    //Teste verifica se o tipo retornado é correto
    public class AnoModeloCaminhao
    {
        public int Id { get; set; }
        public int AnoModelo { get; set; }

        public List<AnoModeloCaminhao> GetAnosModelos()
        {
            List<AnoModeloCaminhao> lam = new List<AnoModeloCaminhao>()
            {
                new AnoModeloCaminhao() { Id = 1, AnoModelo = DateTime.Today.Year },
                new AnoModeloCaminhao() { Id = 2, AnoModelo = DateTime.Today.Year+1 }
            };

            return (lam);
        }

    }
}
