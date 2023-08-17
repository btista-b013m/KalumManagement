using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KalumManagement.Dtos
{
    public class AspiranteDTO
    {
        public string NoExpediente { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estatus { get; set; } = "NO ASIGNADO";
        public virtual ExamenAdmisionDTO ExamenAdmision { get; set; }
        public virtual JornadaDTO Jornada { get; set; }
        public virtual CarreraTecnicaDTO CarreraTecnica { get; set; }
    }
}