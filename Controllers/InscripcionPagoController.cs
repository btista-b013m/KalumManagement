using Microsoft.AspNetCore.Mvc;
using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;
using KalumManagement.Dtos;
using AutoMapper;
using KalumManagement.Services;
using KalumManagement.Models;



namespace KalumManagement.Controllers
{
        
    [ApiController]
    [Route("kalum-management/v1/inscripcion-pago")]
    public class InscripcionPagoController : ControllerBase
    {
        private readonly KalumDBContext dBContext;
        private readonly ILogger<InscripcionPagoController> Logger;

        private readonly IMapper Mapper;
        private readonly IQueueService AspiranteService;
        public InscripcionPagoController(KalumDBContext _dBContext, ILogger<InscripcionPagoController> _logger, IMapper _mapper, IQueueService _queueService)
        {
            this.dBContext = _dBContext;
            this.Logger = _logger;
            this.Mapper = _mapper;
            this.AspiranteService = _queueService;
        }

 
        [HttpGet]
        [ResponseCache(Duration =10)]
        public async Task<ActionResult<IEnumerable<CarreraTecnicaListDTO>>> Get()
        {
            Logger.LogDebug("Iniciando proceso de consulta");
            //List<CarreraTecnica> carreras = await this.dBContext.CarrerasTecnicas.Include(ct => ct.Aspirantes).ToListAsync();
            /*List<CarreraTecnica> carreras = await this.dBContext.CarrerasTecnicas.ToListAsync();
            Logger.LogDebug("Finalizando el proceso de consulta");
            if (carreras == null || carreras.Count == 0)
            {
                Logger.LogDebug("No existen registros actualmente en la base de datos");
                return new NoContentResult();
            }
            Logger.LogDebug("Se ejecuto de forma exitosa la consulta de la información");
            List<CarreraTecnicaListDTO> lista = this.Mapper.Map<List<CarreraTecnicaListDTO>>(carreras);*/
            Guid uuid = Guid.NewGuid();
            return Ok(uuid.ToString());
            //return Ok(lista);
        }

        [HttpGet("{id}", Name = "GetInscripcionPago")]
        public async Task<ActionResult<InscripcionPago>> GetInscripcionPago(string id)
        {
            Logger.LogDebug($"Iniciando el proceso de busq  ueda con id {id}");
            var inscripcion = await dBContext.InscripcionPago.FirstOrDefaultAsync(ct => ct.BoletaPago == id);
            if (inscripcion == null)
            {
                Logger.LogWarning($"No existe registro con el id {id}");
                return new NoContentResult();
            }
            Logger.LogInformation($"Se ejecuta exitosamente la consulta con el id{id}");
            return Ok(inscripcion);
        }

        [HttpPost]
        public async Task<ActionResult<InscripcionPago>> Post([FromBody] InscripcionPagoCreateDTO value)
        {
            Logger.LogDebug($"Iniciando el proceso de registro con la siguiente información{value}");
            InscripcionPago elemento = this.Mapper.Map<InscripcionPago>(value);
            elemento.BoletaPago = Guid.NewGuid().ToString().ToUpper();
            Logger.LogDebug($"Generación de llave con valor{elemento.BoletaPago}");
            await dBContext.InscripcionPago.AddAsync(elemento);
            await dBContext.SaveChangesAsync();
            Logger.LogInformation($"Se ejecuto exitosamnete el proceso de almacenamiento");
            return new CreatedAtRouteResult("GetInscripcionPago", new {id = elemento.BoletaPago}, elemento);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InscripcionPago>> Delete(string id)
        {
            Logger.LogDebug($"Iniciando el proceso de eliminación con el id {id}");
            InscripcionPago inscripcionPago = await this.dBContext.InscripcionPago.FirstOrDefaultAsync(ct => ct.BoletaPago == id);
            if(inscripcionPago == null)
            {
                Logger.LogWarning($"No existe registro con el id {id}");
                return NotFound();
            }
            else
            {
                this.dBContext.InscripcionPago.Remove(inscripcionPago);
                await this.dBContext.SaveChangesAsync();
                Logger.LogInformation("Se ejecuto exitosamente el proceso de eliminación");
                return inscripcionPago;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] InscripcionPago value)
        {
            Logger.LogDebug($"Iniciando el proceso de actualización");
            InscripcionPago inscripcionPago = await this.dBContext.InscripcionPago.FirstOrDefaultAsync(ct => ct.BoletaPago == id);
            if(inscripcionPago == null)
            {
                Logger.LogWarning($"No se encontro ningún registro con el id {id}");
                return NotFound();
            }
            inscripcionPago.BoletaPago= value.BoletaPago;
            this.dBContext.Entry(inscripcionPago).State = EntityState.Modified;
            await this.dBContext.SaveChangesAsync();
            Logger.LogInformation("Se proceso exitosamente la actualización de datos");
            return NoContent();
        }
    }
}