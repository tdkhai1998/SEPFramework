using System;

namespace SEPFramework
{
    class Attribute
    {
        public Attribute(Type t, string n, object v = null)
        {
            this.Type = t;
            this.Name = n;
            this.Value = v;
        }
        private Attribute() { }
        public Type Type;
        public string Name;
        public Object Value;
        public Attribute Clone()
        {
            return new Attribute
            {
                Name = Name,
                Type = Type,
                Value = Value
            };
        }
    }
}
