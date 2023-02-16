using System;
using Common.Interfaces;

namespace ModelPlaneInfo.Entities
{
    /// <summary>
    /// A self-describing class
    /// entity "Airplane Models"
    /// and contains validation methods
    /// input data
    /// </summary>
    [Serializable]
    public partial class ModelPlane : IKeyable, IEntity
    {
        private string name;
        private PlaneType type;
        private int? beginnYear;
        private string used;
        private string description;
        private string note;
        
        #region *** Properties ***
        public string Name
        {
            get { return name; }
            set
            {
                if (!CheckName(value, out string message))
                {
                    throw new ArgumentException(message);
                }
                name = value;
            }
        }
        public int? BeginnYear
        {
            get { return beginnYear; }
            set
            {
                if (!CheckBeginnYear(value, out string message))
                {
                    throw new ArgumentException(message);
                }
                beginnYear = value;
            }
        }
        public string Used
        {
            get { return used; }
            set
            {
                if (!CheckUsed(value, out string message))
                {
                    throw new ArgumentException(message);
                }
                used = value;
            }
        }
        public PlaneType Type { get => type; set => type = value; }
        public string Description
        {
            get { return description; }
            set
            {
                if (!CheckDescription(value, out string message))
                {
                    throw new ArgumentException(message);
                }
                description = value;
            }
        }
        public string Note
        {
            get { return note; }
            set
            {
                if (!CheckNote(value, out string message))
                {
                    throw new ArgumentException(message);
                }
                note = value;
            }
        }
        public string Key {
            get
            {
                return string.Format("{0} {1}", name, type);
            }
        }
        public int Id { get; set; }

        #endregion

        #region *** Constructors ***
        public ModelPlane(string name, PlaneType type,
            int? beginnYear, string used, 
            string description, string note)
        {
            Name = name;
            Type = type;
            BeginnYear = beginnYear;
            Used = used;
            Description = description;
            Note = note;
        }
        public ModelPlane(string name, PlaneType type,
            int? beginnYear, string used)
        {
            Name = name;
            Type = type;
            BeginnYear = beginnYear;
            Used = used;
        }

        public ModelPlane(PlaneType type)
        {
            Type = type;
        }

        public ModelPlane(string name, 
            int? beginnyear, string used)
        {
            Name = name;
            BeginnYear = beginnyear;
            Used = used;
        }

        public ModelPlane(string name, PlaneType type)
               : this(name, type, null, null) { }

        public ModelPlane() { }

        #endregion

    }
}
