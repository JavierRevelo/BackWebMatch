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
    public class ElectricalInformationController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public ElectricalInformationController(AppDBContext context)
        {
            _appDbContext = context;
        }
        /// <summary>
        /// Obtiene toda la información eléctrica.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ElectricalInformationDTO>>> GetAllElectricalInformation()
        {
            // Crear una lista para almacenar la información eléctrica a devolver
            var wellList = new List<ElectricalInformationDTO>();

            // Obtener toda la información de la base de datos
            var listaBase = await _appDbContext.Completes.ToListAsync();

            // Convertir cada entidad en un DTO y agregarlo a la lista
            foreach (var item in listaBase)
            {
                var wellItem = new ElectricalInformationDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(),
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    Required_kVA = item.Required_KVA.ToString(),
                    Surface_voltage = item.Surface_Voltage.ToString(),
                };
                wellList.Add(wellItem);
            }

            // Devolver la lista con estado HTTP 200 OK
            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente la información eléctrica.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateElectricalInformation(int id, [FromBody] UpdateElectricalInformationDTO updateDto)
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
            if (updateDto.Required_kVA != null)
            {
                entity.Required_KVA = double.Parse(updateDto.Required_kVA);
            }
            if (updateDto.Surface_voltage != null)
            {
                entity.Surface_Voltage = double.Parse(updateDto.Surface_voltage);
            }

            // Guardar los cambios realizados en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Devolver el DTO actualizado con estado HTTP 200 OK
            return Ok(updateDto);
        }

        /// <summary>
        /// Elimina la información eléctrica por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricalInformation(int id)
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
