using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Cors;

using Microsoft.Extensions.Logging;
using back_MSI_SuperMami.Models;

namespace back_MSI_SuperMami.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Prog3")]
    public class AreasController : ControllerBase
    {
        private readonly d4nfd5l4d933b1Context _context = new d4nfd5l4d933b1Context();

        private readonly db_SuperMamiFinalContext db = new db_SuperMamiFinalContext();

        private readonly ILogger<AreasController> _logger;


        public AreasController(ILogger<AreasController> logger)
        {
            _logger = logger;
            //_context = context;
        }

        //GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
        {
            return await _context.Areas.ToListAsync();
        }

        //[HttpGet]
        //public ActionResult<ResultadosApi> Get()
        //{
        //    ResultadosApi resultado = new ResultadosApi();
        //    resultado.Ok = true;
        //    resultado.Return = db.Areas.ToList();
        //    return resultado;

        //}


        //[HttpGet]
        //public async Task<ActionResult<string>> getMensaje()
        //{
        //    return "Te pudiste conectar a la db";
        //}


        //Holaaaa



        // GET: api/Areas/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Area>> GetArea(int id)
        //{
        //    var area = await _context.Areas.FindAsync(id);

        //    if (area == null)
        //    {
        //        return NotFound();
        //    }

        //    return area;
        //}

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutArea(int id, Area area)
        //{
        //    if (id != area.Idarea)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(area).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AreaExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Area>> PostArea(Area area)
        //{
        //    _context.Areas.Add(area);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetArea", new { id = area.Idarea }, area);
        //}

        //// DELETE: api/Areas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteArea(int id)
        //{
        //    var area = await _context.Areas.FindAsync(id);
        //    if (area == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Areas.Remove(area);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


    }
}
