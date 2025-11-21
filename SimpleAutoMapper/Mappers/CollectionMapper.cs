using SimpleAutoMapper.Util;
using System.Collections;
using System.Reflection;

namespace SimpleAutoMapper.Mappers
{
    internal static class CollectionMapper
    {
        internal static void Map(object sourceValue, PropertyInfo destProp, object destination)
        {
            var destPropType = destProp.PropertyType;
            var itemType = destPropType.IsArray ? destPropType.GetElementType() : destPropType.GetGenericArguments().FirstOrDefault();

            if (itemType == null)
            {
                return;
            }

            var destinationList = (IList?)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
            if (destinationList == null)
            {
                return;
            }

            foreach (var item in (IEnumerable) sourceValue)
            {
                if (TypeUtil.IsPrimitiveOrSimpleType(itemType) || itemType.IsEnum)
                {
                    destinationList.Add(item);
                    continue;
                }
                var nestedItem = Activator.CreateInstance(itemType);
                if (nestedItem != null)
                {
                    ObjectMapper.Map(item, nestedItem);
                }
                destinationList.Add(nestedItem);
            }

            if (destPropType.IsArray)
            {
                var array = Array.CreateInstance(itemType, destinationList.Count);
                destinationList.CopyTo(array, 0);
                destProp.SetValue(destination, array);
            }
            else
            {
                destProp.SetValue(destination, destinationList);
            }
        }
    }
}
