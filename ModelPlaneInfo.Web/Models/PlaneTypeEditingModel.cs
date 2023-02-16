using ModelPlaneInfo.Entities;
using System.ComponentModel.DataAnnotations;

namespace ModelPlaneInfo.Web.Models
{
    public class PlaneTypeEditingModel
    {
        public int Id { get; set; }

        [Display(Name = "Type name")]
        [Required(ErrorMessage =
             "Need to fill in the field \'Name of the aircraft type\'")]
        [StringLength(25, MinimumLength = 4,
             ErrorMessage = "Name of the aircraft type "
             + "must contain from 4 to 25 characters")]

        public string Name { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        public static explicit operator PlaneTypeEditingModel(PlaneType obj)
        {
            return new PlaneTypeEditingModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Note = obj.Note,
            };
        }

    }
}