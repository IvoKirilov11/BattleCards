using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUS.MvcFramework
{
    public class ServiceCollection : IServiceCollection
    {
        private Dictionary<Type, Type> dependencyContainer = new Dictionary<Type, Type>();
        public void Add<TSource, TDestination>()
        {
            dependencyContainer[typeof(TSource)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if(dependencyContainer.ContainsKey(type))
            {
                type = dependencyContainer[type];
            }
            var constructor = type.GetConstructors().OrderBy(x => x.GetParameters().Count()).FirstOrDefault();
            var parametars = constructor.GetParameters();
            var parmeterValues = new List<object>();
            foreach (var parametar in parametars)
            {
                var parameterValue = CreateInstance(parametar.ParameterType);
                parmeterValues.Add(parameterValue);
            }
            var obj = constructor.Invoke(parmeterValues.ToArray());
            return obj;
        }
    }
}
