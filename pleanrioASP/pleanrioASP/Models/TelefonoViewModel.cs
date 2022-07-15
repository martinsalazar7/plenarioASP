using System.ComponentModel.DataAnnotations;

namespace pleanrioASP.Models
{
    public class TelefonoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Numero de Telefono")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe tener {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Numero { get; set; }
        public int PersonaId{ get; set; }
    }
}
