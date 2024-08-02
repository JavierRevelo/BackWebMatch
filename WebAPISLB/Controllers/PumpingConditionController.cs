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
    public class PumpingConditionController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public PumpingConditionController(AppDBContext context)
        {
            _appDbContext = context;
        }

        /// <summary>
        /// Obtiene todas las condiciones de bombeo.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<PumpingConditionDTO>>> GetAllPumpingCondition()
        {
            var wellList = new List<PumpingConditionDTO>();
            var listaBase = await _appDbContext.Completes.ToListAsync();
            foreach (var item in listaBase)
            {
                var wellItem = new PumpingConditionDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(),
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    Discharge_pressure = item.Discharge_Pressure.ToString(),
                    Frecuency = item.Frecuency.ToString(),
                    Gas_rate_into_pump = item.Gas_Rate_Into_Pump.ToString(),
                    Inlet_gas_volume_fraction = item.Inlet_Gas_Volume_Fraction.ToString(),
                    Intake_pressure = item.Intake_Pressure.ToString(),
                    Total_separation_efficiency = item.Total_Separation_Efficiency.ToString(),
                };
                wellList.Add(wellItem);
            }

            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente PumpingCondition.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePumpingCondition(int id, [FromBody] UpdatePumpingConditionDTO updateDto)
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
            if (updateDto.Discharge_pressure != null)
            {
                entity.Discharge_Pressure = double.Parse(updateDto.Discharge_pressure);
            }
            if (updateDto.Frecuency != null)
            {
                entity.Frecuency = double.Parse(updateDto.Frecuency);
            }
            if (updateDto.Gas_rate_into_pump != null)
            {
                entity.Gas_Rate_Into_Pump = double.Parse(updateDto.Gas_rate_into_pump);
            }
            if (updateDto.Inlet_gas_volume_fraction != null)
            {
                entity.Inlet_Gas_Volume_Fraction = double.Parse(updateDto.Inlet_gas_volume_fraction);
            }
            if (updateDto.Intake_pressure != null)
            {
                entity.Intake_Pressure = double.Parse(updateDto.Intake_pressure);
            }
            if (updateDto.Total_separation_efficiency != null)
            {
                entity.Total_Separation_Efficiency = double.Parse(updateDto.Total_separation_efficiency);
            }

            // Guardar cambios en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Retornar 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Elimina una condición de bombeo por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePumpingCondition(int id)
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
