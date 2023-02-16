using ModelPlaneInfo.Entities;
using System.ComponentModel.DataAnnotations;

namespace ModelPlaneInfo.Web.Models
{
    public class PlaneTypeSelectionModel
    {
        public int Id { get; set; }

        [Display(Name = "Type name")]
        public string Name { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }

        public static explicit operator PlaneTypeSelectionModel(PlaneType obj)
        {
            return new PlaneTypeSelectionModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Note = obj.Note,
            };
        }
    }
}