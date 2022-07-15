using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pleanrioASP.Data;
using pleanrioASP.Data.Entities;
using pleanrioASP.Models;

namespace pleanrioASP.Controllers
{
    public class PersonasController : Controller
    {
        public DataContext _context { get; }
        public PersonasController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string OrdNomb, string Filtro)
        {

            ViewData["CurrentSort"] = OrdNomb;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(OrdNomb) ? "NameDesc" : "";
            ViewData["Filtro"] = Filtro;
            IQueryable<Persona> query = _context.Personas
                .Include(t => t.Telefonos);

            if(string.IsNullOrEmpty(OrdNomb))
            {

                query = query.OrderBy(p => p.Nombre);
            }
            else
            {
                query = query.OrderByDescending(p => p.Nombre);

            }

            if (String.IsNullOrEmpty(Filtro))
            {
                
                return View(await query.ToListAsync());
            }
            else
            {
                return View(await query
                                .Where(p=>p.Nombre.Contains(Filtro))
                                .ToListAsync());
            }

        }
        public IActionResult Create()
        {
            Persona persona = new Persona() { Telefonos = new List<Telefono>() };
            return View(persona);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Personas.Add(persona);
                    int id = await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(persona);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }
            Persona persona = await _context.Personas
                .Include(t => t.Telefonos)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Personas.Update(persona);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(persona);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Persona persona = await _context.Personas
                .Include(t => t.Telefonos)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Persona person)
        {
            if (person != null)
            {
                _context.Personas.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            Persona persona = await _context.Personas
                .Include(t => t.Telefonos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        public async Task<IActionResult> AddTel(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TelefonoViewModel model = new TelefonoViewModel()
            {
                PersonaId = id
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTel(TelefonoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Telefono tel = new()
                    {
                        Numero=model.Numero,
                        Persona = await _context.Personas.FindAsync(model.Id)
                    };
                    _context.Add(tel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details),new { id = model.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }

            return View(model);
        }
        public async Task<IActionResult> EditTel(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }
            Telefono telefono = await _context.Telefonos
                .Include(p=>p.Persona)
                .FirstOrDefaultAsync(p=>p.Id==id);

            TelefonoViewModel model= new()
            {
                Id = telefono.Id,
                Numero= telefono.Numero,
                PersonaId=telefono.Persona.Id
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTel(TelefonoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Telefono tel = new()
                    {
                        Id = model.Id,
                        Numero = model.Numero,
                    };
                    _context.Telefonos.Update(tel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.PersonaId });

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteTel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Telefono tel = await _context.Telefonos.Include(p=>p.Persona)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tel== null)
            {
                return NotFound();
            }

            return View(tel);
        }

        [HttpPost, ActionName("DeleteTel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            

            Telefono tel = await _context.Telefonos.Include(p => p.Persona)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tel != null)
            {
                _context.Telefonos.Remove(tel);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = tel.Persona.Id});
        }

    }
}
