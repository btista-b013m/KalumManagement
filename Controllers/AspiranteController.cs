using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("kalum-management/v1/aspirantes")]
    public class AspiranteController : ControllerBase
    {
        private readonly KalumDBContext dBContext;
        private readonly ILogger<AspiranteController> Logger;

        private readonly IMapper Mapper;
        private readonly IQueueService AspiranteService;

        public AspiranteController(KalumDBContext _dBContext, ILogger<AspiranteController> _logger, IMapper _mapper, IQueueService _queueService)
        {
            this.dBContext = _dBContext;
            this.Logger = _logger;
            this.Mapper = _mapper;
            this.AspiranteService = _queueService;
        }

        //[HttpGet("page/{page}")]
        //public async Task<ActionResult<IEnumerable<PaginationDTO>>> GetCarreraTecnicaPagination(int page)
        //{
        //  var queryable = this.dBContext.CarrerasTecnicas.AsQueryable();
        //int registros = await queryable.CountAsync(); 
        //if(registros == 0)
        //{
        //  return NoContent();     
        //}  
        //else
        //{
        // var carrerasTecnicas = queryable.OrderBy(carrerasTecnicas => carrerasTecnicas.Nombre).Pagination(page).ToListAsync();
        //}
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspiranteListDTO>>> Get()
        {
            Logger.LogDebug("Iniciando proceso de consulta");
            List<Aspirante> aspirantes = await this.dBContext.Aspirante.Include(ct => ct.ExamenAdmision).ToListAsync();

            Logger.LogDebug("Finalizando el proceso de consulta");
            if (aspirantes == null || aspirantes.Count == 0)
            {
                Logger.LogDebug("No existen registros actualmente en la base de datos");
                return new NoContentResult();
            }
            Logger.LogDebug("Se ejecuto de forma exitosa la consulta de la información");
            List<CarreraTecnicaListDTO> lista = this.Mapper.Map<List<CarreraTecnicaListDTO>>(aspirantes);

            return Ok(lista);
        }

        [HttpGet("{id}", Name = "GetAspirante")]
        public async Task<ActionResult<Aspirante>> GetAspirante(string id)
        {
            Logger.LogDebug($"Iniciando el proceso de busq  ueda con id {id}");
            var aspirante = await dBContext.Aspirante.FirstOrDefaultAsync(ct => ct.NoExpediente == id);
            if (aspirante == null)
            {
                Logger.LogWarning($"No existe registro con el id {id}");
                return new NoContentResult();
            }
            Logger.LogInformation($"Se ejecuta exitosamente la consulta con el id{id}");
            return Ok(aspirante);
        }

        [HttpPost]
        [Route("post")]
        public async Task<ActionResult<AspiranteDTO>> Post([FromBody] AspiranteCreateDTO aspiranteCreateDTO)
        {
            this.Logger.LogDebug("Iniciando el proceso para almacenar un registro de aspirante");
            CarreraTecnica carreraTecnica = await this.dBContext.CarrerasTecnicas.FirstOrDefaultAsync(ct => ct.CarreraId == aspiranteCreateDTO.CarreraId);
            if (carreraTecnica == null)
            {
                this.Logger.LogDebug($"No existe la carrera técnica con el id{aspiranteCreateDTO.CarreraId}");
                return BadRequest();
            }
            Jornada jornada = await this.dBContext.Jornadas.FirstOrDefaultAsync(j => j.JornadaId == aspiranteCreateDTO.JornadaId);
            if (jornada == null)
            {
                this.Logger.LogDebug($"No existe la carrera técnica con el id{aspiranteCreateDTO.JornadaId}");
                return BadRequest();
            }
            ExamenAdmision examenAdmision = await this.dBContext.ExamenAdmisiones.FirstOrDefaultAsync(ex => ex.examenid == aspiranteCreateDTO.ExamenId);
            if (jornada == null)
            {
                this.Logger.LogDebug($"No existe la carrera técnica con el id{aspiranteCreateDTO.ExamenId}");
                return BadRequest();
            }

            
            bool resultado = await this.AspiranteService.CrearSolicitudAspiranteAsync(aspiranteCreateDTO);
            this.Logger.LogInformation("Se finalizo el proceso de registro de un aspirante nuevo");
            AspiranteResponse aspiranteResponse = null;
            if(resultado)
            {
               aspiranteResponse = new AspiranteResponse()
               {
                Estatus = "OK",
                Mensaje = "Ha sido creada la solicitud"
               };
            }
            else
            {
                aspiranteResponse = new AspiranteResponse()
               {
                Estatus = "Error",
                Mensaje = "Hubo un error en su solicitu, favor de contactar al administrador"
               };
            }
            return Ok(aspiranteResponse);

            //Aspirante aspirante = this.Mapper.Map<Aspirante>(aspiranteCreateDTO);
            //await this.dBContext.Aspirante.AddAsync(aspirante);
            //await this.dBContext.SaveChangesAsync();
            //AspiranteDTO aspiranteDTO = Mapper.Map<AspiranteDTO>(aspirante);
            //return Ok(aspiranteDTO);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Aspirante>> Delete(string id)
        {
            Logger.LogDebug($"Iniciando el proceso de eliminación con el id {id}");
            Aspirante aspirante = await this.dBContext.Aspirante.FirstOrDefaultAsync(ct => ct.NoExpediente == id);
            if (aspirante == null)
            {
                Logger.LogWarning($"No existe registro con el id {id}");
                return NotFound();
            }
            else
            {
                this.dBContext.Aspirante.Remove(aspirante);
                await this.dBContext.SaveChangesAsync();
                Logger.LogInformation("Se ejecuto exitosamente el proceso de eliminación");
                return aspirante;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Aspirante>> Put(string id, [FromBody] Aspirante value)
        {
            Logger.LogDebug($"Iniciando el proceso de actualización");
            Aspirante aspirante = await this.dBContext.Aspirante.FirstOrDefaultAsync(ct => ct.NoExpediente == id);
            if (aspirante == null)
            {
                Logger.LogWarning($"No se encontro ningún registro con el id {id}");
                return NotFound();
            }
            aspirante.Apellido = value.Apellido;
            aspirante.Nombre = value.Nombre;
            aspirante.Direccion = value.Direccion;
            aspirante.Telefono = value.Telefono;
            aspirante.Email = value.Email;
            aspirante.CarreraId = value.CarreraId;
            aspirante.JornadaId = value.JornadaId;
            aspirante.ExamenId = value.ExamenId;
            this.dBContext.Entry(aspirante).State = EntityState.Modified;
            await this.dBContext.SaveChangesAsync();
            Logger.LogInformation("Se proceso exitosamente la actualización de datos");
            return NoContent();
        }

    }



}