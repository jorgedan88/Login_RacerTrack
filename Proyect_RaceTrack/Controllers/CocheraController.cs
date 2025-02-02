using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyect_RaceTrack.Data;
using Proyect_RaceTrack.Models;
using Proyect_RaceTrack.ViewModels.CocheraViewModels;
using Proyect_RaceTrack.Services;
using Proyect_RaceTrack.ViewModels;
using Proyect_RaceTrack.ViewModels.PistaViewModels;

namespace Proyect_RaceTrack.Controllers
{
    public class CocheraController : Controller
    {
        private ICocheraService _cocheraService;
        private IPistaService _pistaService;

        public CocheraController(ICocheraService cocheraService, IPistaService pistaService)
        {
            _cocheraService = cocheraService;
            _pistaService = pistaService;
        }

        // GET: Cochera
        public IActionResult Index(string NameFilterCoc)
        {
            var model = new CocheraIndexViewModel();
            model.cocheras = _cocheraService.GetAll(NameFilterCoc);

            return View(model);

        }

        // GET: Cochera/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cochera = _cocheraService.GetById(id.Value);
            // .FirstOrDefaultAsync(m => m.AeronaveId == id);
            if (cochera == null)
            {
                return NotFound();
            }

            // var viewModel = new CocheraDetailViewModel();
            // viewModel.CocheraNombre = cochera.CocheraNombre;
            // viewModel.CocheraNumero = cochera.CocheraNumero;
            // viewModel.CocheraSector = cochera.CocheraSector;
            // viewModel.CocheraAptoMantenimiento = cochera.CocheraAptoMantenimiento;
            // viewModel.CocheraOficinas = cochera.CocheraOficinas;
            //viewModel.Pistas = hangar.Pistas;

            return View(cochera);
        }

        // GET: Cochera/Create
        public IActionResult Create()
        {
            // ViewData["Pistas"] = new SelectList(_context.Pista.ToList(),"PistaId","PistaNombre");
            //ViewData["Pistas"] = new SelectList(_pistaService.GetAll(), "PistaId", "PistaNombre", "nameFilterPista");
            ViewData["Pistas"] = new SelectList(_pistaService.GetAll(), "PistaId", "PistaNombre", "nameFilterPista");
            return View();
        }

        // POST: Cochera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CocheraId, CocheraNombre, CocheraNumero, CocheraSector,CocheraAptoMantenimiento,CocheraOficinas,PistaIds")] CocheraCreateViewModel cocheraView)
        {
            if (ModelState.IsValid)
            {
                //var pistas = _context.Pista.Where(x=> hangarView.PistaIds.Contains(x.PistaId)).ToList();
                var pistas = _pistaService.GetAll().Where(x => cocheraView.PistaIds.Contains(x.PistaId)).ToList();

                var cochera = new Cochera
                {
                    CocheraNombre = cocheraView.CocheraNombre,
                    CocheraNumero = cocheraView.CocheraNumero,
                    CocheraSector = cocheraView.CocheraSector,
                    CocheraAptoMantenimiento = cocheraView.CocheraAptoMantenimiento,
                    CocheraOficinas = cocheraView.CocheraOficinas,
                    Pistas = pistas

                };


                _cocheraService.Create(cochera);
                return RedirectToAction(nameof(Index));
            }
            return View(cocheraView);
        }

        // GET: Cochera/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cochera = _cocheraService.GetById(id.Value);
            if (cochera == null)
            {
                return NotFound();
            }
            ViewData["Pistas"] = new SelectList(_pistaService.GetAll(), "PistaId", "PistaNombre", "nameFilterPista");
            return View(cochera);
        }

        // POST: Cochera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CocheraId, CocheraNombre, CocheraNumero, CocheraSector,CocheraAptoMantenimiento,CocheraOficinas,PistaIds")] Cochera cochera)
        {
            if (id != cochera.CocheraId)
            {
                return NotFound();
            }
            //ModelState.Remove("Locales");
            //ModelState.Remove("Talles");
            if (ModelState.IsValid)
            {
                try
                {
                    _cocheraService.Update(cochera);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocheraExists(cochera.CocheraId))
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

            return View(cochera);
        }

        // GET: Cochera/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cochera = _cocheraService.GetById(id.Value);
            if (cochera == null)
            {
                return NotFound();
            }
            var viewModel = new CocheraDeleteViewModel();
            viewModel.CocheraNombre = cochera.CocheraNombre;
            viewModel.CocheraNumero = cochera.CocheraNumero;
            viewModel.CocheraSector = cochera.CocheraSector;
            viewModel.CocheraAptoMantenimiento = cochera.CocheraAptoMantenimiento;
            viewModel.CocheraOficinas = cochera.CocheraOficinas;

            return View(viewModel);

        }

        // POST: Cochera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var cochera = _cocheraService.GetById(id);
            if (cochera != null)
            {
                _cocheraService.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CocheraExists(int id)
        {
            return _cocheraService.GetById(id) != null;
        }
    }
}
