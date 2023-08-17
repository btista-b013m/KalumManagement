using Microsoft.AspNetCore.Mvc;
using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace KalumManagement.Controllers
{
    [ApiController]
    [Route("kalum-management/v1/examen-admision")]
    public class ExamenAdmisionController : ControllerBase
    {
        private readonly KalumDBContext dBContext;
        private readonly ILogger<ExamenAdmisionController> Logger;
        
        public ExamenAdmisionController(KalumDBContext _dBContext, ILogger<ExamenAdmisionController> _logger)
        {
            this.dBContext = _dBContext;
            this.Logger = _logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenAdmision>>> Get()
        {
            Logger.LogDebug("Iniciando proceso de consulta");
            List<ExamenAdmision> Examenes = await dBContext.ExamenAdmisiones.ToListAsync();
            Logger.LogDebug("Finalizando el proceso de consulta");
            if (Examenes == null || Examenes.Count == 0)
            {
                Logger.LogDebug("No existen registros actualmente en la base de datos");
                return new NoContentResult();
            }
            Logger.LogDebug("Se ejecuto de forma exitosa la consulta de la información");
            return Ok(Examenes);
        }

        [HttpGet("{id}", Name = "GetExamenAdmision")]
        public async Task<ActionResult<ExamenAdmision>> GetExamenAdmision(string id)
        {
            Logger.LogDebug($"Iniciando el proceso de busq  ueda con id {id}");
            var examen = await dBContext.ExamenAdmisiones.FirstOrDefaultAsync(ct => ct.examenid == id);
            if (examen == null)
            {
                Logger.LogWarning($"No existe registro con el id {id}");
                return new NoContentResult();
            }
            Logger.LogInformation($"Se ejecuta exitosamente la consulta con el id{id}");
            return Ok(examen);
        }

        [HttpPost]
        public async Task<ActionResult<ExamenAdmision>> Post([FromBody] ExamenAdmision value)
        {
            Logger.LogDebug($"Iniciando el proceso de registro con la siguiente información{value}");
            value.examenid = Guid.NewGuid().ToString().ToUpper();
            Logger.LogDebug($"Generación de llave con valor{value.examenid}");
            await dBContext.ExamenAdmisiones.AddAsync(value);
            await dBContext.SaveChangesAsync();
            Logger.LogInformation($"Se ejecuto exitosamnete el proceso de almacenamiento");
            return new CreatedAtRouteResult("GetExamenAdmision", new {id = value.examenid}, value);
        }
    }
}