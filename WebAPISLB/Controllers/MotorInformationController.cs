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
    public class MotorInformationController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public MotorInformationController(AppDBContext context)
        {
            _appDbContext = context;
        }
        /// <summary>
        /// Obtiene toda la información del motor.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MotorInformationDTO>>> GetAllMotorInformation()
        {
            // Crear una lista para almacenar la información del motor a devolver
            var wellList = new List<MotorInformationDTO>();

            // Obtener toda la información de la base de datos
            var listaBase = await _appDbContext.Completes.ToListAsync();

            // Convertir cada entidad en un DTO y agregarlo a la lista
            foreach (var item in listaBase)
            {
                var wellItem = new MotorInformationDTO
                {
                    id = item.Id,
                    Columna = item.Columna,
                    Fecha = item.Fecha.ToString(),
                    Fecha_Carga = item.Fecha_Carga.ToString(),
                    Pozo = item.Pozo,
                    Motor_horse_power = item.Motor_Horse_Power.ToString(),
                    Motor_amperage = item.Motor_Amperage.ToString(),
                    Motor_voltage = item.Motor_Voltage.ToString(),
                    Load_factor = item.Load_Factor.ToString(),
                    Efficiency = item.Efficiency.ToString(),
                };
                wellList.Add(wellItem);
            }

            // Devolver la lista con estado HTTP 200 OK
            return Ok(wellList);
        }

        /// <summary>
        /// Actualiza parcialmente la información del motor.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMotorInformation(int id, [FromBody] UpdateMotorInformationDTO updateDto)
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
            if (updateDto.Motor_horse_power != null)
            {
                entity.Motor_Horse_Power = double.Parse(updateDto.Motor_horse_power);
            }
            if (updateDto.Motor_amperage != null)
            {
                entity.Motor_Amperage = double.Parse(updateDto.Motor_amperage);
            }
            if (updateDto.Motor_voltage != null)
            {
                entity.Motor_Voltage = double.Parse(updateDto.Motor_voltage);
            }
            if (updateDto.Load_factor != null)
            {
                entity.Load_Factor = double.Parse(updateDto.Load_factor);
            }
            if (updateDto.Efficiency != null)
            {
                entity.Efficiency = double.Parse(updateDto.Efficiency);
            }

            // Guardar los cambios realizados en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Devolver estado HTTP 204 No Content para indicar que la operación fue exitosa
            return NoContent();
        }

        /// <summary>
        /// Elimina la información del motor por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorInformation(int id)
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
