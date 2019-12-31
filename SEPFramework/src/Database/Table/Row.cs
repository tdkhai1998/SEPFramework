using System.Collections.Generic;

namespace SEPFramework
{
    public class Row
    {
        public Row(Table table)
        {
            AddProtoType(table.Columns);

        }
        private Row() { }

      

        private Dictionary<string, Attribute> attributes = new Dictionary<string, Attribute>();

        internal Dictionary<string, Attribute> Attributes { get => attributes; }

        public object this[string attributeName]
        {
            get
            {
                if (attributes.ContainsKey(attributeName))
                {
                    return attributes[attributeName].Value;
                }
                return null;
            }
            set
            {
                if (attributes.ContainsKey(attributeName))
                {
                    if (attributes.ContainsKey(attributeName))
                    {
                        attributes[attributeName].Value = value;
                    }
                }
            }
        }

        private void AddProtoType(List<Column> columns)
        {
            this.attributes = new Dictionary<string, Attribute>();
            for (int i = 0; i < columns.Count; i++)
            {
                attributes.Add(columns[i].Name, new Attribute(columns[i].Type, columns[i].Name,columns[i].ReadOnly));
            }
        }
        public Row Clone()
        {
            Row r = new Row();
            foreach (KeyValuePair<string, Attribute> item in attributes)
            {
                r.attributes.Add(item.Key, item.Value.Clone());
            }
            return r;
        }

    }
}