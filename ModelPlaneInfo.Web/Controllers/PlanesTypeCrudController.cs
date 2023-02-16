using Common.Repositories.Interfaces;
using ModelPlaneInfo.Entities;
using ModelPlaneInfo.Repositories.Interfaces;
using ModelPlaneInfo.Web.Infrastructure;
using ModelPlaneInfo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ModelPlaneInfo.Web.Controllers
{
    public class PlanesTypeCrudController : Controller
    {
        public IInfoUnitOfWork Uow { get; set; }

        public IRepository<PlaneType> repository;
        private IEnumerable<PlaneTypeSelectionModel> SelectionModelObjects { get; set; }
        private IEnumerable<PlaneTypeEditingModel> EditingModelObjects { get; set; }
        public IRepository<PlaneType> Repository
        {
            get { return repository; }
            set
            {
                repository = value;
                SelectionModelObjects = repository.GetAll()
                    .Select(e => (PlaneTypeSelectionModel)e).OrderBy(e => e.Name);
                EditingModelObjects = repository.GetAll()
                    .Select(e => (PlaneTypeEditingModel)e).OrderBy(e => e.Name);
            }
        }
      
        public PlanesTypeCrudController()
        {
            Uow = UoWCreator.GetInstance();
            Repository = Uow.PlanesTypeRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Message = TempData["message"];
            return View(SelectionModelObjects);
        }

        #region *** Create ***
        public ActionResult Create()
        {
            return View(new PlaneTypeEditingModel());
        }

        [HttpPost]
        public ActionResult Create(PlaneTypeEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                Repository.Add((PlaneType)CreateEntityObject(model));
                Uow.Save();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View(model);
            }
            TempData["message"] = string.Format(
                $"New type plane \"{ model.Name}\" saved");
            return RedirectToAction("Index", "PlanesTypeCrud");
        }

        private object CreateEntityObject(PlaneTypeEditingModel model)
        {
            PlaneType entityObject = new PlaneType
            {
                Name = model.Name,
                Note = model.Note
            };
            return entityObject;
        }
        #endregion

        #region *** Edit ***
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PlaneTypeEditingModel model = EditingModelObjects.First(e => e.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PlaneTypeEditingModel model)
        {
            try
            {
                var entityObject = Repository.GetAll().First(e => e.Id == model.Id);
                UpdateEntityObject(entityObject, model);
                Uow.Save();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View(model);
            }
            TempData["message"] = string.Format(
                "Changing aircraft type data \"{0}\" saved", model.Name);
            return RedirectToAction("Index", "PlanesTypeCrud");
        }

        private void UpdateEntityObject(PlaneType entityObject,
            PlaneTypeEditingModel model)
        {
            entityObject.Name = model.Name;
            entityObject.Note = model.Note;
        }
        #endregion

        #region *** Delete ***
        public ActionResult Delete(int id)
        {
            var model = EditingModelObjects.First(e => e.Id == id);
            PlaneType entityObject = Repository.GetAll().First(e => e.Id == model.Id);
            Repository.Delete(entityObject);
            Uow.Save();
            return RedirectToAction("Index");
        }
        #endregion 

    }
}