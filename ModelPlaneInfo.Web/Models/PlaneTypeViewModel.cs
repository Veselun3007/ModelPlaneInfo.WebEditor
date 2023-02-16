using ModelPlaneInfo.Entities;
using System.ComponentModel.DataAnnotations;

namespace ModelPlaneInfo.Web.Models
{
    public class PlaneTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Type name")]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public bool HasInfo { get; set; }

        public static explicit operator 
            PlaneTypeViewModel(PlaneType obj)
        {
            return new PlaneTypeViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                HasInfo = !string.IsNullOrWhiteSpace(obj.Note)
            };
        }
    }
}