using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPISLB.Context;
using WebAPISLB.DTO;
using WebAPISLB.DTO.UpdateDTO;
using WebAPISLB.Models;

namespace WebAPISLB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellBoreController : ControllerBase
    {

        private readonly AppDBContext _appDbContext;
        public WellBoreController(AppDBContext context)
        {
            _appDbContext = context;
        }

        /// <summary>
        /// Obtiene todas las condiciones del pozo.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<WellBoreDTO>>> GetAllWellbore()
        {
            var wellList = new List<WellBoreDTO>();
            var listaBase = await _appDbContext.Completes.ToListAsync();
            foreach (var item in listaBase)
            {
                var wellItem = new WellBoreDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(),
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    Wellhead_pressure = item.Wellhead_Pressure.ToString(),
                };
                wellList.Add(wellItem);
            }

            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente la información del pozo.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateWellBore(int id, [FromBody] UpdateWellBoreDTO updateDto)
        {
            // Buscar la entidad en la base de datos
            var entity = await _appDbContext.Completes.FindAsync(id);
            if (entity == null)
            {
                // Retornar 404 si no se encuentra la entidad
                return NotFound();
            }

            // Actualizar solo los campos que no son nulos en el DTO
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
            if (updateDto.Wellhead_pressure != null)
            {
                entity.Wellhead_Pressure = double.Parse(updateDto.Wellhead_pressure);
            }

            // Guardar cambios en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Retornar 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Elimina una información del pozo por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWellBore(int id)
        {
            // Buscar la entidad en la base de datos
            var entity = await _appDbContext.Completes.FindAsync(id);
            if (entity == null)
            {
                // Retornar 404 si no se encuentra la entidad
                return NotFound();
            }

            // Eliminar la entidad
            _appDbContext.Completes.Remove(entity);
            // Guardar cambios en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Retornar 204 No Content
            return NoContent();
        }
    }
}
