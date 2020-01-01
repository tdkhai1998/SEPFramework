using System;

namespace SEPFramework
{
    public class Column
    {
        public Column(string name, Type type, bool readOnly)
        {
            this.Name = name;
            this.Type = type;
            this.ReadOnly = readOnly;
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