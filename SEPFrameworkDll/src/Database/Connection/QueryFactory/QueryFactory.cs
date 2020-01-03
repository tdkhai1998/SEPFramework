using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class QueryFactory
    {
        public static QueryAbstractFactory GetFactory(QueryType type)
        {
            switch (type)
            {
                case QueryType.insert:
                    return new InsertFactory();
                case QueryType.delete:
                    return new DeleteFactory();
                case QueryType.update:
                    return new UpdateFactory();
                default:
                    return null;
            }
        }
    }
}
