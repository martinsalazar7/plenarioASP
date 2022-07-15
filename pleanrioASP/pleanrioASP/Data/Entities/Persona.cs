using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pleanrioASP.Data.Entities
{
    public class Persona
    {
        public int Id { get; set; }

        [Display(Name ="Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Credito Maximo")]
        [Column(TypeName = "decimal(20,2)")] 
        public decimal CreditoMaximo{ get; set; }
        public ICollection<Telefono> Telefonos { get; set; }
    }
}
