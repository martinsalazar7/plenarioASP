using System.ComponentModel.DataAnnotations;

namespace pleanrioASP.Data.Entities
{
    public class Telefono
    {
        public int Id { get; set; }
        public Persona Persona { get; set; }
        
        [Display(Name = "Telefono")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Numero { get; set; }


    }
}
