using System;
using SimpleInjector;

namespace SoftPlan.Juros.Domain
{

    public class Kernel
    {
        private static Container _kernel;

        public static Container GetKernel()
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            return _kernel;
        }
        public static void StartKernel()
        {
            _kernel = new Container();
        }


        private static void StartBase()
        {
            _kernel = new Container();

        }

        public static T Get<T>() where T : class
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            return _kernel.GetInstance<T>();
        }

        public static void Bind<TFrom, TTo>()
            where TTo : class, TFrom
            where TFrom : class
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            _kernel.Register<TFrom, TTo>();
        }

        public static void Bind(Type type1, Type type2)
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            _kernel.Register(type1, type2);
        }
    }
}