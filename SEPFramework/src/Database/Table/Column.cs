using System;

namespace SEPFramework
{
    public class Column
    {
        public Column(string name, Type type, bool readOnly)
        {
            Name = name;
            Type = type;
            ReadOnly = readOnly;
        }
        public readonly string Name;
        public readonly Type Type;
        public readonly bool ReadOnly;
        public Attribute CreateAtrribute(object value)
        {
            return new Attribute(Type, Name, ReadOnly, value);
        }
    }
}