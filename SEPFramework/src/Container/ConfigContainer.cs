using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    public class SEPContainer
    {
        public static void RegisterForm<TInterface, TImplementation>() where TImplementation : BaseForm, TInterface
        {
            MyContainer.RegisterType<TInterface, TImplementation>();
        }
        public static void RegisterConnection(CommonConnection conn)
        {
            MyContainer.RegisterInstance<CommonConnection>(conn);
        }
        public static void RegisterRole(Role role)
        {
            MyContainer.RegisterInstance<Role>(role);
        }
    }
}