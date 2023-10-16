using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaServicios.Web.Data;
using AgendaServicios.Web.Models;

namespace AgendaServicios.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string apellido, string nombre, int? numeroDocumento)
        {
			//var clientes = _context.Clientes
			//        .Include(c => c.Localidad)
			//        .Include(c => c.Provincia);

			//return View(await clientes.ToListAsync());

			IQueryable<Cliente> clientes = _context.Clientes.Include(c => c.Localidad).Include(c => c.Provincia);

			if (!string.IsNullOrEmpty(nombre))
			{
				clientes = clientes.Where(c => c.Nombre.Contains(nombre));
			}
			if (!string.IsNullOrEmpty(apellido))
			{
				clientes = clientes.Where(c => c.Apellido.Contains(apellido));
			}

			// validar que el numero de documento sea un numero
			if (numeroDocumento != null)
			{
				clientes = clientes.Where(c => c.NumeroDocumento == numeroDocumento);
			}

			ViewBag.Nombre = nombre;
			ViewBag.Apellido = apellido;
			ViewBag.NumeroDocumento = numeroDocumento;

			return View(await clientes.ToListAsync());
		}

		// GET: Clientes/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Localidad)
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["LocalidadId"] = new SelectList(_context.Localidades, "LocalidadId", "Descripcion");
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Apellido,Nombre,FechaNacimiento,TipoDocumento,NumeroDocumento,Calle,Altura,Barrio,Partido,ProvinciaId,LocalidadId,CodigoPostal,CuitCuil,RazonSocial,CorreoElectronico,Celular,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalidadId"] = new SelectList(_context.Localidades, "LocalidadId", "Descripcion", cliente.LocalidadId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Descripcion", cliente.ProvinciaId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["LocalidadId"] = new SelectList(_context.Localidades, "LocalidadId", "Descripcion", cliente.LocalidadId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Descripcion", cliente.ProvinciaId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Apellido,Nombre,FechaNacimiento,TipoDocumento,NumeroDocumento,Calle,Altura,Barrio,Partido,ProvinciaId,LocalidadId,CodigoPostal,CuitCuil,RazonSocial,CorreoElectronico,Celular,Telefono")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalidadId"] = new SelectList(_context.Localidades, "LocalidadId", "Descripcion", cliente.LocalidadId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Descripcion", cliente.ProvinciaId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Localidad)
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'AppDbContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.ClienteId == id)).GetValueOrDefault();
        }
    }
}
