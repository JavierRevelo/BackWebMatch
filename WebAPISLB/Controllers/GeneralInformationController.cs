using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPISLB.Context;
using WebAPISLB.DTO.UpdateDTO;
using WebAPISLB.Models;

namespace WebAPISLB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralInformationController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public GeneralInformationController(AppDBContext context)
        {
            _appDbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneralInformation>>> GetAllingformation()
        {
            var ListaInformacion = await _appDbContext.Generals.ToListAsync();
            return Ok(ListaInformacion);
        }


        [HttpPost]
        public async Task<ActionResult<GeneralInformation>> AddNewGeneralInformation(GeneralInformation gl)
        {
            _appDbContext.Generals.Add(gl);
            await _appDbContext.SaveChangesAsync();
            return Ok(gl);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRevisado(int id, [FromBody] UpdateRevisadoDTO updateRevisadoDTO)
        {
            var info = await _appDbContext.Generals.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }

            info.Check = updateRevisadoDTO.Revisado;
            await _appDbContext.SaveChangesAsync();

            return NoContent(); // HTTP 204 No Content
        }



    }
}
