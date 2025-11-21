using SimpleAutoMapper.Util;
using System.Collections;
using System.Reflection;

namespace SimpleAutoMapper.Mappers
{
    internal static class ObjectMapper
    {
        internal static void Map(object source, object destination)
        {
            var sourceType = source.GetType();
            var destinationType = destination.GetType();

            foreach (var destProp in destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!destProp.CanWrite)
                {
                    continue;
                }

                var sourceProp = sourceType.GetProperty(destProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp == null || !sourceProp.CanRead)
                {
                    continue;
                }

                var sourceValue = sourceProp.GetValue(source);
                if (sourceValue == null)
                {
                    continue;
                }

                var destPropType = destProp.PropertyType;

                if (TypeUtil.IsPrimitiveOrSimpleType(destPropType) || destPropType.IsEnum)
                {
                    destProp.SetValue(destination, sourceValue);
                    continue;
                }
                else if (typeof(IEnumerable).IsAssignableFrom(destPropType))
                {
                    CollectionMapper.Map(sourceValue, destProp, destination);
                    continue;
                }

                var nestedDestination = Activator.CreateInstance(destPropType);
                if (nestedDestination != null)
                {
                    Map(sourceValue, nestedDestination);
                }
                destProp.SetValue(destination, nestedDestination);
            }
        }
    }
}
