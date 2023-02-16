using Common.Repositories;
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
    public class ModelPlaneCrudController : Controller
    {
        public IInfoUnitOfWork Uow { get; set; }
        private IEnumerable<ModelPlaneSelectionModel> SelectionModelObjects { get; set; }
        private IEnumerable<ModelPlaneEditingModel> EditingModelObjects { get; set; }
        public IEnumerable<PlaneType> PlaneTypes { get; set; }
        public IEnumerable<ModelPlane>  ModelPlanes { get; set; }

        public IRepository<ModelPlane>  modelsPlane;
        public IRepository<ModelPlane> ModelsPlane
        {
            get { return modelsPlane; }
            set
            {
                modelsPlane = value;
                SelectionModelObjects = modelsPlane.GetAll().Select(e => (ModelPlaneSelectionModel)
                    e).OrderBy(e => e.Name);
                EditingModelObjects = modelsPlane.GetAll().Select(e => (ModelPlaneEditingModel)
                    e).OrderBy(e => e.Name);
            }
        }

        public ModelPlaneCrudController()
        {
            Uow = UoWCreator.GetInstance();
            ModelsPlane = Uow.ModelPlanesRepository;
            ModelPlanes = Uow.ModelPlanesRepository.GetAll();
            PlaneTypes = Uow.PlanesTypeRepository.GetAll();
        }


        public ActionResult Index()
        {
            ViewBag.Message = TempData["message"];
            return View(SelectionModelObjects);
        }

        #region *** Create ***
        public ActionResult Create()
        {
            ViewBag.TypeName 
                = CreatePlanesTypeNamesSelectList();
            ViewBag.Used = CreateUsedSelectList();
            return View(new ModelPlaneEditingModel());
        }

        [HttpPost]
        public ActionResult Create(ModelPlaneEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TypeName = CreatePlanesTypeNamesSelectList();
                ViewBag.Used = CreateUsedSelectList();
                return View(model);
            }
            try
            {
                Uow.ModelPlanesRepository.Add(
                    (ModelPlane)CreateEntityObject(model));
                Uow.Save();
            }
            catch (Exception ex)
            {
                ViewBag.TypeName = CreatePlanesTypeNamesSelectList();
                ViewBag.Used = CreateUsedSelectList();
                ModelState.AddModelError("", ex);
                return View(model);
            }
            TempData["message"] = string.Format(
                $"Aircraft model data \"{model.Name}\" saved");
            return RedirectToAction("Index", "ModelPlaneCrud");
        }
      

        private object CreateEntityObject(ModelPlaneEditingModel model)
        {
            ModelPlane entityObject = new ModelPlane
            {
                Name = model.Name,
                Type = PlaneTypes.First
                (e => e.Name == model.TypeName),
                BeginnYear = model.BeginnYear,
                Used = model.Used,
                Note = model.Note,
                Description = model.Description
            };
            return entityObject;

        }
        #endregion

        #region *** Edit ***
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ModelPlaneEditingModel model = 
                EditingModelObjects.First(e => e.Id == id);
            ViewBag.TypeName = 
                CreatePlanesTypeNamesSelectList(model.TypeName);
            ViewBag.Used = CreateUsedSelectList(model.Used);
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(ModelPlaneEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TypeName = 
                    CreatePlanesTypeNamesSelectList(model.TypeName);
                ViewBag.Used = 
                    CreateUsedSelectList(model.Used);
                return View(model);
            }
            try
            {
                var entityObject =
                    ModelsPlane.GetAll().First(e => e.Id == model.Id);
                UpdateEntityObject(entityObject, model);
                Uow.Save();
            }
            catch (Exception ex)
            {
                ViewBag.TypeName =
                    CreatePlanesTypeNamesSelectList();
                ViewBag.Used = CreateUsedSelectList();
                ModelState.AddModelError("", ex);
                return View(model);
            }
            TempData["message"] = string.Format(
                $"Changing data about the aircraft model \"{model.TypeName}\" saved");
            return RedirectToAction("Index");
        }
       

        private void UpdateEntityObject(ModelPlane entityObject,
            ModelPlaneEditingModel model)
        {
            entityObject.Name = model.Name;
            entityObject.Type.Name = model.TypeName;
            entityObject.BeginnYear = model.BeginnYear;
            entityObject.Used = model.Used;
            entityObject.Note = model.Note;
            entityObject.Description = model.Description;
        }
        #endregion

        #region *** Delete ***
        public ActionResult Delete(int id)
        {
            var model = EditingModelObjects.First(e => e.Id == id);
            ModelPlane entityObject = ModelsPlane.GetAll().First(e => e.Id == model.Id);
            ModelsPlane.Delete(entityObject);
            Uow.Save();
            return RedirectToAction("Index");
        }
        #endregion

        #region *** Create SelectList ***
        private dynamic CreatePlanesTypeNamesSelectList
            (string selectedValue = "")
        {
            List<string> values = new List<string>();
            values.AddRange(PlaneTypes.Select(e => e.Name));
            List<SelectListItem> list =
                new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e,
                    Selected = e == selectedValue
                });
            }
            return list;
        }

        private dynamic CreateUsedSelectList
            (string selectedValue = "")
        {
            string[] values = new string[] 
            { "Yes", "No", "Unknown" };
            List<SelectListItem> list =
                new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e,
                    Selected = e == selectedValue
                });
            }
            return list;
        }
        #endregion
    }
}
