using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyect_RaceTrack.Data;
using Proyect_RaceTrack.Models;
using Proyect_RaceTrack.ViewModels.PistaViewModels;
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
        public IActionResult Index(string nameFilter)
        {
            var model = new VehiculoIndexViewModel();
            model.vehiculos = _vehiculoService.GetAll(nameFilter);

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
        public IActionResult Create([Bind("VehiculoNombre,VehiculoApellido,VehiculoMatricula,VehiculoFabricacion, VehiculoTipo")] VehiculoCreateViewModel vehiculoView)
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

            var viewModel = new VehiculoEditViewModel();
            viewModel.VehiculoNombre = vehiculo.VehiculoNombre;
            viewModel.VehiculoApellido = vehiculo.VehiculoApellido;
            viewModel.VehiculoMatricula = vehiculo.VehiculoMatricula;
            viewModel.VehiculoTipo = vehiculo.VehiculoTipo;
            viewModel.VehiculoFabricacion = vehiculo.VehiculoFabricacion;
            return View(viewModel);

            // return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("VehiculoId,VehiculoFabricacion,VehiculoTipo,VehiculoMatricula")] Vehiculo vehiculoView)
        {
            if (id != vehiculoView.VehiculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _vehiculoService.Update(vehiculoView);
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculoView);
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
        // public IActionResult UpdatePrice(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var aeronave = _vehiculoService.GetById(id.Value);
        //     if (aeronave == null)
        //     {
        //         return NotFound();
        //     }

        //     var viewModel = new MenuUpdatePriceViewModel
        //     {
        //         AeronaveCosto = vehiculo.AeronaveCosto,
        //         AeronaveId = vehiculo.AeronaveId,
        //         AeronaveTipo = vehiculo.AeronaveTipo,
        //     };

        //     return View(viewModel);
        // }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult UpdatePrice(MenuUpdatePriceViewModel model)
        // {
        //     var aeronave = _vehiculoService.GetById(model.AeronaveId);
        //     if (aeronave == null)
        //     {
        //         return NotFound();
        //     }
        //     if (model.Cantidad > 0)
        //     {
        //         aeronave.AeronaveCosto = (model.Cantidad * aeronave.AeronaveCosto) + model.Instruccion;
        //         _aeronaveService.Update(aeronave);

        //     }

        //     return RedirectToAction("Index");
        // }

    }
}
