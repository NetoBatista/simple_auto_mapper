using System.Collections;
using System.Reflection;

namespace SimpleAutoMapper
{
    public static class AutoMapper
    {
        public static TDestination? Map<TDestination>(object source) where TDestination : new()
        {
            if (source == null)
            {
                return default;
            }

            if (source is not IEnumerable || !typeof(TDestination).GetInterfaces().Contains(typeof(IEnumerable)))
            {
                var destination = new TDestination();
                MapObject(source, destination);
                return destination;
            }

            var sourceList = (IEnumerable)source;
            var itemType = typeof(TDestination).GetGenericArguments()[0];
            var destinationList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType))!;

            foreach (var item in sourceList)
            {
                var mappedItem = MapItem(itemType, item);
                destinationList.Add(mappedItem);
            }

            return (TDestination)destinationList;
        }

        private static object? MapItem(Type destinationType, object source)
        {
            if (source == null)
            {
                return null;
            }

            if (IsPrimitiveOrSimpleType(destinationType) || destinationType.IsEnum)
            {
                return source;
            }

            var destination = Activator.CreateInstance(destinationType);
            if (destination != null)
            {
                MapObject(source, destination);
            }

            return destination;
        }

        private static void MapObject(object source, object destination)
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

                if (IsPrimitiveOrSimpleType(destPropType) || destPropType.IsEnum)
                {
                    destProp.SetValue(destination, sourceValue);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(destPropType))
                {
                    MapCollection(sourceValue, destProp, destination);
                }
                else
                {
                    var nestedDestination = Activator.CreateInstance(destPropType);
                    if (nestedDestination != null)
                    {
                        MapObject(sourceValue, nestedDestination);
                    }
                    destProp.SetValue(destination, nestedDestination);
                }
            }
        }

        private static void MapCollection(object sourceValue, PropertyInfo destProp, object destination)
        {
            var destPropType = destProp.PropertyType;
            var itemType = destPropType.IsArray ? destPropType.GetElementType() : destPropType.GetGenericArguments().FirstOrDefault();

            if (itemType == null)
            {
                return;
            }

            var sourceCollection = (IEnumerable)sourceValue;
            var destinationList = (IList?)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
            if (destinationList == null)
            {
                return;
            }

            foreach (var item in sourceCollection)
            {
                if (IsPrimitiveOrSimpleType(itemType) || itemType.IsEnum)
                {
                    destinationList.Add(item);
                    continue;
                }
                var nestedItem = Activator.CreateInstance(itemType);
                if (nestedItem != null)
                {
                    MapObject(item, nestedItem);
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

        private static bool IsPrimitiveOrSimpleType(Type type)
        {
            return type.IsEnum ||
                type.IsPrimitive ||
                new[]
                {
                typeof(string),
                typeof(Guid),
                typeof(DateTime),
                typeof(decimal),
                typeof(double),
                typeof(int),
                typeof(long),
                typeof(char)
                }.Contains(type) ||
                Nullable.GetUnderlyingType(type) != null;
        }
    }
}