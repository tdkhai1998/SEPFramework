using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    internal class MyContainer
    {
        private static MyContainer instance = null;
        private static readonly object padlock = new object();

        MyContainer()
        {
            types.Add(typeof(IAddForm), typeof(AddForm));
            types.Add(typeof(IReadForm), typeof(ReadForm));
            types.Add(typeof(IUpdateForm), typeof(UpdateForm));
        }

        private static MyContainer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MyContainer();
                    }
                    return instance;
                }
            }
        }
        private readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();
        public static void RegisterType<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Instance.IRegisterType<TInterface, TImplementation>();
        }
        public static void RegisterInstance<TInterface>(object obj)
        {
            Instance.IRegisterInstance<TInterface>(obj);
        }

        public static TInterface Create<TInterface>()
        {
            return Instance.ICreate<TInterface>();
        }


        private void IRegisterType<TInterface, TImplementation>() where TImplementation : TInterface  
        {
            types[typeof(TInterface)] = typeof(TImplementation);
        }


        private void IRegisterInstance<TInterface>(object obj)
        {
            instances[typeof(TInterface)] = obj;
        }


        private TInterface ICreate<TInterface>()
        {
            if (instances.ContainsKey(typeof(TInterface)))
            {
                return (TInterface)instances[typeof(TInterface)];
            }
            else
            {
                return (TInterface)Create(typeof(TInterface));
            }
        }

        private object Create(Type type)
        {
            if (instances.ContainsKey(type)) return instances[type];
            else if (types.ContainsKey(type))
            {
                Type concreteType = types[type];
                ConstructorInfo defaultConstructor = concreteType.GetConstructors()[0];
                ParameterInfo[] defaultParams = defaultConstructor.GetParameters();
                object[] parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();
                return defaultConstructor.Invoke(parameters);
            }
            else
            {
                ConstructorInfo defaultConstructor = type.GetConstructors()[0];
                ParameterInfo[] defaultParams = defaultConstructor.GetParameters();
                object[] parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();
                return defaultConstructor.Invoke(parameters);
            }
        }

    }
}
