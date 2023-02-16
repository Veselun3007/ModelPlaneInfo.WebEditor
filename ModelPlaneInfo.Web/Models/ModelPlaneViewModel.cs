using ModelPlaneInfo.Entities;
using System.ComponentModel.DataAnnotations;

namespace ModelPlaneInfo.Web.Models
{
    public class ModelPlaneViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name of the aircraft")]
        public string Name { get; set; }

        [Display(Name = "Type of aircraft")]
        public string Type { get; set; }

        [Display(Name = "Year of start of operation")]
        public int? BeginnYear { get; set; }

        [Display(Name = "Availability in operation")]
        public string Used { get; set; }

        [ScaffoldColumn(false)]
        public bool HasInfo { get; set; }

        public static explicit operator 
            ModelPlaneViewModel(ModelPlane obj)
        {
            return new ModelPlaneViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Type = obj.Type.Name,
                BeginnYear = obj.BeginnYear,
                Used = obj.Used,
                HasInfo = !string.
                IsNullOrWhiteSpace(obj.Note)
                || !string.
                IsNullOrWhiteSpace(obj.Description)
            };
        }
    }
}