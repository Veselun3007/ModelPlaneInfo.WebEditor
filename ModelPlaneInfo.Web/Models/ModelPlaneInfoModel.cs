using ModelPlaneInfo.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ModelPlaneInfo.Web.Models
{
    public class ModelPlaneInfoModel
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

        public string[] Info { get; private set; }

        private static string[] CreateInfo(ModelPlane obj)
        {
            string s = null;
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                s += "Примітка: " + obj.Note + "\n";
            }
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                s += "Опис\n" + obj.Description;
            }
            string[] info = null;
            if (s != null)
            {
                info = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries).ToArray();
            }
            return info;
        }

        public static explicit operator ModelPlaneInfoModel(ModelPlane obj)
        {
            return new ModelPlaneInfoModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Type = obj.Type.Name,
                BeginnYear = obj.BeginnYear,
                Used = obj.Used,
                Info = CreateInfo(obj)
            };
        }
    }
}