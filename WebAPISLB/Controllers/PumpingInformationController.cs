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
    public class PumpingInformationController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public PumpingInformationController(AppDBContext context)
        {
            _appDbContext = context;
        }
        /// <summary>
        /// Obtiene toda la información de bombeo.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<PumpingInformationDTO>>> GetAllPumpingInformation()
        {
            var wellList = new List<PumpingInformationDTO>();
            var listaBase = await _appDbContext.Completes.ToListAsync();
            foreach (var item in listaBase)
            {
                var wellItem = new PumpingInformationDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(),
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    Required_power = item.Required_Power.ToString(),
                    Pump_efficiency = item.Pump_Efficiency.ToString(),
                };
                wellList.Add(wellItem);
            }

            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente la información de bombeo.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePumpingInformation(int id, [FromBody] UpdatePumpingInformationDTO updateDto)
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
            if (updateDto.Required_power != null)
            {
                entity.Required_Power = double.Parse(updateDto.Required_power);
            }
            if (updateDto.Pump_efficiency != null)
            {
                entity.Pump_Efficiency = double.Parse(updateDto.Pump_efficiency);
            }

            // Guardar cambios en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Retornar 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Elimina una información de bombeo por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePumpingInformation(int id)
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
