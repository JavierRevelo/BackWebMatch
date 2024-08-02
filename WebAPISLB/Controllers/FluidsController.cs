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
    public class FluidsController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public FluidsController(AppDBContext context)
        {
            _appDbContext = context;
        }
        /// <summary>
        /// Obtiene toda la información de fluidos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<FluidsDTO>>> GetAllFluids()
        {
            // Crear una lista para almacenar la información de fluidos a devolver
            var wellList = new List<FluidsDTO>();

            // Obtener toda la información de la base de datos
            var listaBase = await _appDbContext.Completes.ToListAsync();

            // Convertir cada entidad en un DTO y agregarlo a la lista
            foreach (var item in listaBase)
            {
                var wellItem = new FluidsDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(), // Aplicar formato yy-MM-dd
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    GLR = item.GLR.ToString(),
                    GOR = item.GOR.ToString(),
                    Oil_gravity = item.Oil_Gravity.ToString(),
                    Water_cut = item.Water_Cut.ToString(),
                };
                wellList.Add(wellItem);
            }

            // Devolver la lista con estado HTTP 200 OK
            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente la información de fluidos.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateFluids(int id, [FromBody] UpdateFluidsDTO updateDto)
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
            if (updateDto.GLR != null)
            {
                entity.GLR = double.Parse(updateDto.GLR);
            }
            if (updateDto.GOR != null)
            {
                entity.GOR = double.Parse(updateDto.GOR);
            }
            if (updateDto.Oil_gravity != null)
            {
                entity.Oil_Gravity = double.Parse(updateDto.Oil_gravity);
            }
            if (updateDto.Water_cut != null)
            {
                entity.Water_Cut = double.Parse(updateDto.Water_cut);
            }

            // Guardar los cambios realizados en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Devolver estado HTTP 204 No Content para indicar que la operación fue exitosa
            return NoContent();
        }

        /// <summary>
        /// Elimina la información de fluidos por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFluids(int id)
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
