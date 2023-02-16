using ModelPlaneInfo.Entities;
using ModelPlaneInfo.Web.Infrastructure;
using ModelPlaneInfo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelPlaneInfo.Web.Controllers
{
    public class PlanesTypeController : Controller
    {
        public IEnumerable<PlaneType> Planes { get; set; }
        private readonly IEnumerable<PlaneTypeViewModel> planeTypeViewModels;

        public PlanesTypeController()
        {
            Planes = UoWCreator.GetInstance().PlanesTypeRepository.GetAll();
            planeTypeViewModels = Planes.Select(e => (PlaneTypeViewModel)e)
                   .OrderBy(e => e.Name);
        }

        public ViewResult PlanesType()
        {
            return View(planeTypeViewModels);
        }

        public PartialViewResult _NoteInfo(int id)
        {
            var obj = Planes.First(e => e.Id == id);
            string[] model = null;
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                string s = "Примітка\n" + obj.Note;
                model = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return PartialView(model);
        }

        string[] GetInfo(int id)
        {
            var obj = Planes.First(e => e.Id == id);
            string s = null;
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                s += "Примітка: " + "\n" + obj.Note + "\n";
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