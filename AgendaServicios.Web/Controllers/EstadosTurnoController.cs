﻿using System;
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
    public class EstadosTurnoController : Controller
    {
        private readonly AppDbContext _context;

        public EstadosTurnoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstadosTurno
        public async Task<IActionResult> Index()
        {
            var estadosTurnos = _context.EstadosTurnos.ToList();
            return View(estadosTurnos);
               
        }

        // GET: EstadosTurno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var estadosTurno = await _context.EstadosTurnos
                .FirstOrDefaultAsync(m => m.EstadoTurnoId == id);
            if (estadosTurno == null)
            {
                return NotFound();
            }

            return View(estadosTurno);
        }

        // GET: EstadosTurno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadosTurno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoTurnoId,Descripcion")] EstadosTurno estadosTurno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosTurno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadosTurno);
        }

        // GET: EstadosTurno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var estadosTurno = await _context.EstadosTurnos.FindAsync(id);
            if (estadosTurno == null)
            {
                return NotFound();
            }
            return View(estadosTurno);
        }

        // POST: EstadosTurno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoTurnoId,Descripcion")] EstadosTurno estadosTurno)
        {
            if (id != estadosTurno.EstadoTurnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadosTurno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosTurnoExists(estadosTurno.EstadoTurnoId))
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
            return View(estadosTurno);
        }

        // GET: EstadosTurno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosTurno = await _context.EstadosTurnos
                .FirstOrDefaultAsync(m => m.EstadoTurnoId == id);
            if (estadosTurno == null)
            {
                return NotFound();
            }

            return View(estadosTurno);
        }

        // POST: EstadosTurno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosTurno = await _context.EstadosTurnos.FindAsync(id);
            if (estadosTurno != null)
            {
                _context.EstadosTurnos.Remove(estadosTurno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosTurnoExists(int id)
        {
          return (_context.EstadosTurnos?.Any(e => e.EstadoTurnoId == id)).GetValueOrDefault();
        }
    }
}
