using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    static class Validate
    {
        public static bool IsNumericType(string type)
        {
            switch (type)
            {
                case "Int32":
                case "Int16":
                    return true;
                default:
                    return false;
            }
        }
    }
}
