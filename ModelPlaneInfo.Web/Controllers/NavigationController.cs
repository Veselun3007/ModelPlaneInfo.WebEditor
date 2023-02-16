using ModelPlaneInfo.Entities;
using ModelPlaneInfo.Web.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ModelPlaneInfo.Web.Controllers
{
    public class NavigationController : Controller
    {
        public IEnumerable<ModelPlane> ModelPlanes{ get; set; }

        public NavigationController()
        {
            ModelPlanes = UoWCreator.GetInstance().ModelPlanesRepository.GetAll();
        }

        public const string ALL_CATEGORIES = "...";

        [ChildActionOnly]
        public PartialViewResult ModelPlanesByTypeNameMenu(
                string categoryName = ALL_CATEGORIES)
        {
            ViewBag.SelectedCategoryName = categoryName;
            List<string> categoryNames = new List<string>
            {
                ALL_CATEGORIES
            };
            categoryNames.AddRange(ModelPlanes
                    .Select(e => e.Type.Name)
                    .Distinct().OrderBy(e => e));
            return PartialView(categoryNames);
        }
    }
}