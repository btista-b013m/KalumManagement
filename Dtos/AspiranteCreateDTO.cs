using System.ComponentModel.DataAnnotations;
using KalumManagement.Helpers;


namespace KalumManagement.Dtos
{
    public class AspiranteCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        //[PrimeraLetraMayusculaAttibute]
        public string Apellido {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        //[PrimeraLetraMayusculaAttibute]
        public string Nombre {get;set;}
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        public string Direccion {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Telefono {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]

        [EmailAddress]
        public string Email {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]

        public string Estatus {get;set;} = "No Asignado";
        public string CarreraId {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string JornadaId {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ExamenId {get;set;}
        
    }
          
}