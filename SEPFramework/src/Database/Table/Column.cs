using System;

namespace SEPFramework
{
    public class Column
    {

        public Column(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }
        public readonly string Name;
        public readonly Type Type;
    }
}