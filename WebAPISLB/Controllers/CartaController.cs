using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;
using System;
using WebAPISLB.Context;
using WebAPISLB.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPISLB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public CartaController(AppDBContext context)
        {
            _appDbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CartaModel>>> GetAllCartas()
        {
            var ListaCartas = await _appDbContext.Cartas.ToListAsync();
            return Ok(ListaCartas);
        }


    }


}
