using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SiteManager.Api.Controllers
{
    [ApiController]
    [Route("api/siniestros")]
    [Produces("application/json")]
    public class SiniestroApiController : ControllerBase
    {
        private readonly SiniestroService _service;

        public SiniestroApiController(SiniestroService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtener todos los siniestros",
            Description = "Devuelve la lista completa de siniestros registrados en el sistema.")]
        [SwaggerResponse(200, "Lista de siniestros obtenida correctamente")]
        public IActionResult GetAll()
        {
            return Ok(_service.ObtenerTodos());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtener siniestro por ID",
            Description = "Devuelve la información detallada de un siniestro específico.")]
        [SwaggerResponse(200, "Siniestro encontrado")]
        [SwaggerResponse(404, "Siniestro no encontrado")]
        public IActionResult GetById(int id)
        {
            var siniestro = _service.ObtenerPorId(id);
            if (siniestro == null) return NotFound();
            return Ok(siniestro);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crear un nuevo siniestro",
            Description = "Registra un nuevo siniestro en el sistema.")]
        [SwaggerResponse(200, "Siniestro creado correctamente")]
        [SwaggerResponse(400, "Datos inválidos")]
        public IActionResult Create([FromBody] Siniestro siniestro)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _service.Agregar(siniestro);
            return Ok(siniestro);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualizar un siniestro existente",
            Description = "Actualiza los datos de un siniestro por su ID.")]
        [SwaggerResponse(204, "Siniestro actualizado correctamente")]
        [SwaggerResponse(404, "Siniestro no encontrado")]
        public IActionResult Update(int id, [FromBody] Siniestro siniestro)
        {
            if (id != siniestro.Id) return BadRequest();
            var existente = _service.ObtenerPorId(id);
            if (existente == null) return NotFound();
            _service.Actualizar(siniestro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Eliminar un siniestro",
            Description = "Elimina un siniestro del sistema por su ID.")]
        [SwaggerResponse(200, "Siniestro eliminado correctamente")]
        [SwaggerResponse(404, "Siniestro no encontrado")]
        public IActionResult Delete(int id)
        {
            var existente = _service.ObtenerPorId(id);
            if (existente == null) return NotFound();
            _service.Eliminar(id);
            return Ok();
        }
    }
}