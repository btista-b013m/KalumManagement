using Microsoft.AspNetCore.Mvc;
using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace KalumManagement.Controllers
{
    [ApiController]
    [Route("kalum-management/v1/jornadas")]
    public class JornadaController : ControllerBase
    {
        private readonly KalumDBContext dBContext;
        private readonly ILogger<JornadaController> logger;
        
        public JornadaController(KalumDBContext _dBContext, ILogger<JornadaController> _logger)
        {
            this.dBContext = _dBContext;
            this.logger = _logger;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jornada>>> Get()
        {
            logger.LogDebug("Iniciando proceso de consulta");
            List<Jornada> jornadas = await dBContext.Jornadas.ToListAsync();
            logger.LogDebug("Finalizando el proceso de consulta");
            if (jornadas == null || jornadas.Count == 0)
            {
                logger.LogDebug("No existen registros actualmente");
                return new NoContentResult();
            }
            logger.LogDebug("Se ejecuto de forma exitosa la consulta");
            return Ok(jornadas);
        }

        [HttpGet("{id}", Name = "GetJornada")]
        public async Task<ActionResult<Jornada>> GetJornada(string id)
        {
            var jornada = await dBContext.Jornadas.FirstOrDefaultAsync(j => j.JornadaId == id);
            if (jornada == null)
            {
                return new NoContentResult();
            }
            return Ok(jornada);
        }

        [HttpPost]
        public async Task<ActionResult<Jornada>> Post([FromBody] Jornada value)
        {
            value.JornadaId = Guid.NewGuid().ToString().ToUpper();
            await dBContext.Jornadas.AddAsync(value);
            await dBContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetJornada", new {id = value.JornadaId}, value);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Jornada>> Delete(string id)
        {
            logger.LogDebug($"Iniciando el proceso de eliminación de jornada con el id {id}");
            Jornada jornada = await this.dBContext.Jornadas.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if(jornada == null)
            {
                logger.LogWarning($"No existe registro con el id {id}");
                return NotFound();
            }
            else
            {
                this.dBContext.Jornadas.Remove(jornada);
                await this.dBContext.SaveChangesAsync();
                logger.LogInformation("Se ejecuto exitosamente el proceso de eliminación de la jornada");
                return jornada;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Jornada value)
        {
            logger.LogDebug($"Iniciando el proceso de actualización");
            Jornada jornada = await this.dBContext.Jornadas.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if(jornada == null)
            {
                logger.LogWarning($"No se encontro ningún registro con el id {id}");
                return NotFound();
            }
            jornada.NombreCorto = value.NombreCorto;
            this.dBContext.Entry(jornada).State = EntityState.Modified;
            await this.dBContext.SaveChangesAsync();
            logger.LogInformation("Se proceso exitosamente la actualización de datos de la jornada");
            return NoContent();
        }
    }
}