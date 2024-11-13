using Examen3POO.Data;
using Examen3POO.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen3POO.Data
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options) : base(options) { }

        public DbSet<Inventario> dispositivos { get; set; }
    }   
}
