//using Proyect_RaceTrack.ViewModels;
//using Proyect_RaceTrack.ViewModels.PistaViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyect_RaceTrack.Data;
using Proyect_RaceTrack.Models;

using Proyect_RaceTrack.Services;
using Proyect_RaceTrack.ViewModels;
using Proyect_RaceTrack.ViewModels.VehiculoViewModels;

namespace Proyect_RaceTrack.Controllers
{
    public class VehiculoController : Controller
    {
        private IVehiculoService _vehiculoService;
        public VehiculoController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;

        }

        // GET: Vehiculo
        public IActionResult Index(string NameFilterVeh)
        {
            var model = new VehiculoIndexViewModel();
            model.vehiculos = _vehiculoService.GetAll(NameFilterVeh);

            return View(model);

        }

        // GET: Vehiculo/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = _vehiculoService.GetById(id.Value);
            // .FirstOrDefaultAsync(m => m.AeronaveId == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            var viewModel = new VehiculoDetailViewModel();
            viewModel.VehiculoNombre = vehiculo.VehiculoNombre;
            viewModel.VehiculoApellido = vehiculo.VehiculoApellido;
            viewModel.VehiculoMatricula = vehiculo.VehiculoMatricula;
            viewModel.VehiculoTipo = vehiculo.VehiculoTipo;
            viewModel.VehiculoFabricacion = vehiculo.VehiculoFabricacion;
            return View(viewModel);
        }

        // GET: Vehiculo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("VehiculoNombre,VehiculoApellido,VehiculoMatricula,VehiculoFabricacion, VehiculoTipo, VehiculoCosto")] VehiculoCreateViewModel vehiculoView)
        {
            if (ModelState.IsValid)
            {
                var vehiculo = new Vehiculo
                {
                    VehiculoNombre = vehiculoView.VehiculoNombre,
                    VehiculoApellido = vehiculoView.VehiculoApellido,
                    VehiculoMatricula = vehiculoView.VehiculoMatricula,
                    VehiculoFabricacion = vehiculoView.VehiculoFabricacion,
                    VehiculoTipo = vehiculoView.VehiculoTipo,
                    VehiculoCosto = vehiculoView.VehiculoCosto
                };

                _vehiculoService.Create(vehiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculoView);
        }

        // GET: Vehiculo/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = _vehiculoService.GetById(id.Value);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("VehiculoId,VehiculoNombre,VehiculoApellido,VehiculoMatricula,VehiculoFabricacion, VehiculoTipo, VehiculoCosto")] Vehiculo vehiculo)
        {
            if (id != vehiculo.VehiculoId)
            {
                return NotFound();
            }
            //ModelState.Remove("Locales");
            //ModelState.Remove("Talles");
            if (ModelState.IsValid)
            {
                try
                {
                    _vehiculoService.Update(vehiculo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.VehiculoId))
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

            return View(vehiculo);
        }


        // GET: Vehiculo/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = _vehiculoService.GetById(id.Value);
            if (vehiculo == null)
            {
                return NotFound();
            }
            var viewModel = new VehiculoDeleteViewModel();
            viewModel.VehiculoNombre = vehiculo.VehiculoNombre;
            viewModel.VehiculoApellido = vehiculo.VehiculoApellido;
            viewModel.VehiculoMatricula = vehiculo.VehiculoMatricula;
            viewModel.VehiculoTipo = vehiculo.VehiculoTipo;
            viewModel.VehiculoFabricacion = vehiculo.VehiculoFabricacion;

            return View(viewModel);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var vehiculo = _vehiculoService.GetById(id);
            if (vehiculo != null)
            {
                _vehiculoService.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _vehiculoService.GetById(id) != null;
        }
        //FUNCIONALIDAD /
        public IActionResult UpdatePrice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = _vehiculoService.GetById(id.Value);
            if (vehiculo == null)
            {
                return NotFound();
            }

            var viewModel = new MenuUpdatePriceViewModel
            {
                VehiculoCosto = vehiculo.VehiculoCosto,
                VehiculoId = vehiculo.VehiculoId,
                VehiculoTipo = vehiculo.VehiculoTipo,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePrice(MenuUpdatePriceViewModel model)
        {//Analisar desde aca por el null de vehiculo
            var vehiculo = _vehiculoService.GetById(model.VehiculoId);
            if (vehiculo == null)
            {
                return NotFound();
            }
            if (model.Cantidad > 0)
            {
                vehiculo.VehiculoCosto = (model.Cantidad * vehiculo.VehiculoCosto) + model.Instruccion;
                _vehiculoService.Update(vehiculo); 
            }

            return RedirectToAction("Index");
        }

    }
}
