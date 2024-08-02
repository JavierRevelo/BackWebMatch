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
    public class HeatRiseController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public HeatRiseController(AppDBContext context)
        {
            _appDbContext = context;
        }
        /// <summary>
        /// Obtiene toda la información de aumento de temperatura de bobina.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<HeatRiseDTO>>> GetAllHeatRise()
        {
            // Crear una lista para almacenar la información de aumento de temperatura de bobina a devolver
            var wellList = new List<HeatRiseDTO>();

            // Obtener toda la información de la base de datos
            var listaBase = await _appDbContext.Completes.ToListAsync();

            // Convertir cada entidad en un DTO y agregarlo a la lista
            foreach (var item in listaBase)
            {
                var wellItem = new HeatRiseDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(),
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    Total_winding_temperature = item.Total_Winding_Temperature.ToString(),
                };
                wellList.Add(wellItem);
            }

            // Devolver la lista con estado HTTP 200 OK
            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente la información de aumento de temperatura de bobina.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateHeatRise(int id, [FromBody] UpdateHeatRiseDTO updateDto)
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
                entity.Fecha = DateOnly.Parse(updateDto.Fecha);
            }
            if (updateDto.Fecha_Carga != null)
            {
                entity.Fecha_Carga = DateOnly.Parse(updateDto.Fecha_Carga);
            }
            if (updateDto.Pozo != null)
            {
                entity.Pozo = updateDto.Pozo;
            }
            if (updateDto.Total_winding_temperature != null)
            {
                entity.Total_Winding_Temperature = double.Parse(updateDto.Total_winding_temperature);
            }

            // Guardar los cambios realizados en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Devolver estado HTTP 204 No Content para indicar que la operación fue exitosa
            return NoContent();
        }

        /// <summary>
        /// Elimina la información de aumento de temperatura de bobina por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeatRise(int id)
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
