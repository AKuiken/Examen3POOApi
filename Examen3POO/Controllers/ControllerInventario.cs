using Examen3POO.Data;
using Examen3POO.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen3POO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerInventario : Controller
    {
        private readonly InventarioContext _context;

        public ControllerInventario(InventarioContext context)
        {
            _context = context;
        }

        // GET: api/ControllerInventario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetDispositivos()
        {
            // Devuelve todos los dispositivos en la base de datos
            return await _context.dispositivos.ToListAsync();
        }

        // GET: api/ControllerInventario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetDispositivo(int id)
        {
            // Busca un dispositivo por ID
            var dispositivo = await _context.dispositivos.FindAsync(id);

            if (dispositivo == null)
                return NotFound(); // Si no se encuentra, devuelve NotFound

            return dispositivo;
        }

        // POST: api/ControllerInventario
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostDispositivo(Inventario dispositivo)
        {
            // Agrega un nuevo dispositivo a la base de datos
            _context.dispositivos.Add(dispositivo);
            await _context.SaveChangesAsync();

            // Devuelve el dispositivo creado, con la URL del nuevo recurso
            return CreatedAtAction(nameof(GetDispositivo), new { id = dispositivo.Id }, dispositivo);
        }

        // PUT: api/ControllerInventario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispositivo(int id, Inventario dispositivo)
        {
            if (id != dispositivo.Id)
                return BadRequest(); // Si los ID no coinciden, devuelve un BadRequest

            _context.Entry(dispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Intenta guardar los cambios
            }
            catch (DbUpdateConcurrencyException)
            {
                // Si hay un error de concurrencia, verifica si el dispositivo existe
                if (!_context.dispositivos.Any(d => d.Id == id))
                    return NotFound(); // Si no existe, devuelve NotFound

                throw;
            }

            return NoContent(); // Devuelve NoContent cuando la actualización es exitosa
        }

        // DELETE: api/ControllerInventario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispositivo(int id)
        {
            // Busca el dispositivo a eliminar por ID
            var dispositivo = await _context.dispositivos.FindAsync(id);
            if (dispositivo == null)
                return NotFound(); // Si no existe, devuelve NotFound

            // Elimina el dispositivo de la base de datos
            _context.dispositivos.Remove(dispositivo);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve NoContent al eliminar exitosamente
        }
    }
}
