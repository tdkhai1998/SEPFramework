using System;

namespace SEPFramework
{
    public  class Attribute:Column
    {
        public object Value = null;
        public Attribute(Type type, string name, bool isReadOnly, object value = null):base(name,type,isReadOnly)
        {
            Value = value;
        }

        public Attribute Clone()
        {
            return new Attribute(Type, Name, ReadOnly, Value);
        }
    }
}
