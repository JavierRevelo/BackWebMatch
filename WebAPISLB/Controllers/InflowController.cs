using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPISLB.Context;
using WebAPISLB.DTO;
using WebAPISLB.DTO.UpdateDTO;

namespace WebAPISLB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InflowController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public InflowController(AppDBContext context)
        {
            _appDbContext = context;
        }

        /// <summary>
        /// Obtiene toda la información de flujo.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<InflowDTO>>> GetAllInflow()
        {
            // Crear una lista para almacenar la información de flujo a devolver
            var wellList = new List<InflowDTO>();

            // Obtener toda la información de la base de datos
            var listaBase = await _appDbContext.Completes.ToListAsync();

            // Convertir cada entidad en un DTO y agregarlo a la lista
            foreach (var item in listaBase)
            {
                var wellItem = new InflowDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(),
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    Method = item.Methoth,
                    Pi = item.PI.ToString(),
                    Static_bottombole_pressure = item.Static_Bottombole_Pressure.ToString(),
                };
                wellList.Add(wellItem);
            }

            // Devolver la lista con estado HTTP 200 OK
            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente la información de flujo.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateInflow(int id, [FromBody] UpdateInflowDTO updateDto)
        {
            // Buscar la entidad en la base de datos por ID
            var entity = await _appDbContext.Completes.FindAsync(id);
            if (entity == null)
            {
                // Retornar 404 Not Found si la entidad no existe
                return NotFound();
            }

            // Actualizar los campos que se proporcionan en el DTO
            if (updateDto.Columna != null)
            {
                entity.Columna = updateDto.Columna;
            }
            if (updateDto.Fecha != null)
            {
                entity.Fecha = DateOnly .Parse(updateDto.Fecha);
            }
            if (updateDto.Fecha_Carga != null)
            {
                entity.Fecha_Carga = DateOnly.Parse(updateDto.Fecha_Carga);
            }
            if (updateDto.Pozo != null)
            {
                entity.Pozo = updateDto.Pozo;
            }
            if (updateDto.Method != null)
            {
                entity.Methoth = updateDto.Method;
            }
            if (updateDto.Pi != null)
            {
                entity.PI = double.Parse(updateDto.Pi);
            }
            if (updateDto.Static_bottombole_pressure != null)
            {
                entity.Static_Bottombole_Pressure = double.Parse(updateDto.Static_bottombole_pressure);
            }

            // Guardar los cambios realizados en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Devolver estado HTTP 204 No Content para indicar que la operación fue exitosa
            return NoContent();
        }

        /// <summary>
        /// Elimina la información de flujo por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInflow(int id)
        {
            // Buscar la entidad en la base de datos por ID
            var entity = await _appDbContext.Completes.FindAsync(id);
            if (entity == null)
            {
                // Retornar 404 Not Found si la entidad no existe
                return NotFound();
            }

            // Eliminar la entidad de la base de datos
            _appDbContext.Completes.Remove(entity);
            // Guardar los cambios realizados en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Devolver estado HTTP 204 No Content para indicar que la operación fue exitosa
            return NoContent();
        }
    }
}
