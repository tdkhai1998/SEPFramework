using System;

namespace SEPFramework
{
    class Attribute
    {
    
    
        public Type Type;
        public string Name;
        public Object Value = null;
        public bool IsReadOnly;

        public Attribute(Type type, string name, bool isReadOnly, object value = null)
        {
            Type = type;
            Name = name;
            Value = value;
            IsReadOnly = isReadOnly;
        }

        public Attribute Clone()
        {
            return new Attribute(Type, Name, IsReadOnly, Value);
        }
    }
}
