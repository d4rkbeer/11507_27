using System.Collections.Concurrent;
using System.Reflection;

namespace ControlWork
{

    public static class TypeFactory
    {
        public static object CreateAndFill(Type type, Dictionary<string, object> values)
        {
            object obj = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                if (prop.CanWrite && values.TryGetValue(prop.Name, out object value))
                {
                    try
                    {
                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                    }
                    catch { }
                }
            }
            return obj;
        }
    }

    
}



