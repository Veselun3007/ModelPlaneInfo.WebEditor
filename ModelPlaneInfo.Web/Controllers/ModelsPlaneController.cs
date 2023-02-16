using ModelPlaneInfo.Entities;
using ModelPlaneInfo.Web.Infrastructure;
using ModelPlaneInfo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ModelPlaneInfo.Web.Controllers
{
    public class ModelsPlaneController : Controller
    {
        private IEnumerable<ModelPlane> models { get; set; }
        private IEnumerable<ModelPlaneTableModel> modelPlaneTableModels;
        private IEnumerable<ModelPlaneInfoModel> modelPlaneinfoModelObjects;
        private IEnumerable<ModelPlaneViewModel> modelPlaneViewModels;

        public IEnumerable<ModelPlane> Models
        {
            get { return models; }
            set
            {
                models = value;
                modelPlaneTableModels = models.Select(e =>
                (ModelPlaneTableModel)e)
                    .OrderBy(e => e.Type);
                modelPlaneinfoModelObjects = models.Select(e =>
                (ModelPlaneInfoModel)e)
                    .OrderBy(e => e.Name);
                modelPlaneViewModels = models.Select(e =>
                (ModelPlaneViewModel)e)
                    .OrderBy(e => e.Type);
            }
        }
        public int ItemsPerPage { get; private set; }

        public ModelsPlaneController()
        {
            Models = UoWCreator.GetInstance()
                .ModelPlanesRepository.GetAll();
            ItemsPerPage = 4;
        }


        public ViewResult ModelsPlane()
        {
            return View(modelPlaneViewModels);
        }

        public PartialViewResult _NoteInfo(int id)
        {
            var obj = Models.First(e => e.Id == id);
            string[] model = null;
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                string s = "Примітка\n" + obj.Note;
                model = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return PartialView(model);
        }

        public PartialViewResult _DescriptiveInfo(int id)
        {
            var obj = Models.First(e => e.Id == id);
            string[] model = null;
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                string s = "Опис\n" + obj.Description;
                model = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return PartialView(model);
        }


        public ViewResult InfoWithPaging(
            string pageKey = "..", int pageNumber = 0)
        {
            IEnumerable<ModelPlane> model = Models;
            if (!string.IsNullOrEmpty(pageKey) 
                && pageKey != "..")
            {
                model = model.Where(e => e.Type.Name[0]
                .ToString() == pageKey);
            }
            if (pageNumber != 0)
            {
                model = model
                    .Skip((pageNumber - 1) 
                    * ItemsPerPage)
                    .Take(ItemsPerPage);
            }
            return View(model);
        }

        public ViewResult ModelPlanesByTypeNameInfo(
        string categoryName = 
            NavigationController.ALL_CATEGORIES)
        {
            IEnumerable<ModelPlane> models = Models
                .OrderBy(e => e.Type.Name);
            if (!string.IsNullOrEmpty(categoryName) &&
                categoryName != 
                NavigationController.ALL_CATEGORIES)
            {
                models = models
                    .Where(e => e.Type.Name == categoryName);
            }
            ViewBag.SelectedCategoryName = categoryName;
            return View(models);
        }

        public ActionResult Selection()
        {
            ViewBag.selTypeName = 
                CreatePlanesTypeNamesSelectList();
            ViewBag.selUsed =
                CreateUsedSelectList();
            return View(modelPlaneTableModels);
        }

        const string ALL_VALUES = "...";

        public PartialViewResult _SelectData(string selName, 
            string selTypeName, int? beginnYearFrom,
          int? beginnYearTo, string selUsed)
        {
            var model = modelPlaneTableModels;
            if (!string.IsNullOrWhiteSpace(selName))
                model = model.Where(e => e.Name.ToLower()
                    .StartsWith(selName.ToLower()));

            if (selTypeName != null && selTypeName != ALL_VALUES)
                model = model.Where(e => e.Type == selTypeName);

            if (beginnYearFrom.HasValue)
                model = model.Where(e => 
                e.BeginnYear >= beginnYearFrom.Value);

            if (beginnYearTo.HasValue)
                model = model.Where(e => 
                e.BeginnYear <= beginnYearTo.Value);

            if (selUsed != null && selUsed != ALL_VALUES)
                model = model.Where(e => e.Used == selUsed);

            System.Threading.Thread.Sleep(2000);
            return PartialView("_TableBody", model);
        }

        private dynamic CreatePlanesTypeNamesSelectList()
        {
            List<string> values = new List<string>
            {
                ALL_VALUES
            };
            values.AddRange(Models.Select(e => 
            e.Type.Name).Distinct());
            List<SelectListItem> list =
                new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e,
                });
            }
            return list;
        }

        private dynamic CreateUsedSelectList()
        {            
            string[] values = new string[] { "...", "Так", "Ні", "Невідомо"};
            List<SelectListItem> list =
                new List<SelectListItem>();
            foreach (string e in values)
            {
                list.Add(new SelectListItem
                {
                    Text = e,
                    Value = e,
                });
            }
            return list;
        }

        public const string ALL_PAGE_LINK_NAME = "..";

        public ActionResult BrowseByLetters()
        {
            ViewBag.Letters = new[] { ALL_PAGE_LINK_NAME }
                .Concat(Models
                    .Select(e => e.Name[0].ToString())
                    .Distinct().OrderBy(e => e));
            ViewData["Letters"] = new[] { ALL_PAGE_LINK_NAME }
                .Concat(Models
                    .Select(e => e.Name[0].ToString())
                    .Distinct().OrderBy(e => e));
            return View(modelPlaneinfoModelObjects);

        }

        public PartialViewResult _GetDataByLetter(string selLetter)
        {
            var model = modelPlaneinfoModelObjects;
            if (selLetter != null && selLetter !=
                ALL_PAGE_LINK_NAME)
                model = model.Where(e => 
                e.Name[0] == selLetter[0]);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_BrowseData", model);
        }

        string[] GetInfo(int id)
        {
            var obj = Models.First(e => e.Id == id);
            string s = null;
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                s += "Примітка:" + "\n" + obj.Note ;
            }
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                s += "\n" + "Опис:" + "\n" + obj.Description;
            }
            string[] info = null;
            if (s != null)
            {
                info = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return info;
        }

        [HttpPost]
        public JsonResult JsonInfo(int id)
        {
            var info = GetInfo(id);
            System.Threading.Thread.Sleep(2000);
            return Json(info);
        }
        [HttpPost]
        public JsonResult JsonIdInfo(int id)
        {
            var info = GetInfo(id);
            return Json(new { Id = id, Info = info }); 
        }
    }
}