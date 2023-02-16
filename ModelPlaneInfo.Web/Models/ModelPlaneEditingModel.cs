using ModelPlaneInfo.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelPlaneInfo.Web.Models
{
    public class ModelPlaneEditingModel
    {
        public int Id { get; set; }

        [Display(Name = "Name of the aircraft")]
        [Required(ErrorMessage =
             "Need to fill in the field \'Name of the aircraft\'")]
        [StringLength(40, MinimumLength = 4,
             ErrorMessage = "Name of the aircraft "
             + "must contain from 4 to 40 characters")]

        public string Name { get; set; }

        [Display(Name = "Type of aircraft")]
        [Required(ErrorMessage =
             "Need to fill in the field \'The name of the aircraft type\'")]
        public string TypeName { get; set; }

        [Display(Name = "Year of start of operation")]


        public int? BeginnYear { get; set; }

        [Display(Name = "Availability in operation")]
        [NotMapped]
        public string Used { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        public static explicit operator
            ModelPlaneEditingModel(ModelPlane obj)
        {
            return new ModelPlaneEditingModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                TypeName = obj.Type.Name,
                BeginnYear = obj.BeginnYear,
                Used = obj.Used,
                Note = obj.Note,
                Description = obj.Description,
            };
        }
    }
}