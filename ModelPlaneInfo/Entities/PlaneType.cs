using Common.Interfaces;
using System;

namespace ModelPlaneInfo.Entities
{
    /// <summary>
    /// A self-describing class
    /// entity "Aircraft Types"
    /// and contains validation methods
    /// input data
    /// </summary>
   
    [Serializable]
    public partial class PlaneType : IEntity, IKeyable
    {
        private string name;
        private string note;

        #region *** Properties ***
        public string Key
        {
            get
            {
                return string.Format("{0}", name);
            }
        }
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
        public int Id { get; set; }
        #endregion

        #region *** Constructors ***
        public PlaneType(string name, string note)
        {
            this.Name = name;
            this.Note = note;
        }
        public PlaneType(string name)
        {
            this.Name = name;
        }
        public PlaneType() { }
        #endregion
    }
}
