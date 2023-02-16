using ModelPlaneInfo.Entities;
using System.ComponentModel.DataAnnotations;

namespace ModelPlaneInfo.Web.Models
{
    public class ModelPlaneTableModel
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

        public static explicit operator
            ModelPlaneTableModel(ModelPlane obj)
        {
            return new ModelPlaneTableModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Type = obj.Type.Name,
                BeginnYear = obj.BeginnYear,
                Used = obj.Used,
            };
        }
    }
}