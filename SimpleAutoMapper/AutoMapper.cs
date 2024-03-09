using System.Collections;

namespace SimpleAutoMapper
{
    public class AutoMapper
    {
        public static T? Map<T>(object source)
        {
            if (source is not IEnumerable sourceList || !typeof(T).GetInterfaces().Contains(typeof(IEnumerable)))
            {
                return (T?)Map(source, typeof(T));
            }

            var itemType = typeof(T).GetGenericArguments()[0];
            var targetList = (IList?)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

            if (targetList == null)
            {
                return (T?)targetList;
            }

            foreach (var sourceObject in sourceList)
            {
                var targetObject = Map(sourceObject, itemType);
                if (targetObject != null)
                {
                    targetList.Add(targetObject);
                }
            }

            return (T?)targetList;
        }

        private static object? Map(object source, Type targetType)
        {
            var target = Activator.CreateInstance(targetType);
            var sourceType = source.GetType();
            var sourceProperties = sourceType.GetProperties();
            var targetProperties = targetType.GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var targetProperty in targetProperties)
                {
                    if (sourceProperty.Name != targetProperty.Name)
                    {
                        break;
                    }

                    if (sourceProperty.PropertyType.IsGenericType &&
                        sourceProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var itemType = targetProperty.PropertyType.GetGenericArguments()[0];
                        var sourceList = (IList?)sourceProperty.GetValue(source);
                        var targetList = (IList?)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));

                        if (sourceList == null || targetList == null)
                        {
                            break;
                        }

                        foreach (var item in sourceList)
                        {
                            targetList.Add(Map(item, itemType));
                        }

                        targetProperty.SetValue(target, targetList);
                        break;
                    }

                    if (sourceProperty.PropertyType.IsPrimitive ||
                        sourceProperty.PropertyType.IsEnum ||
                        sourceProperty.PropertyType.Equals(typeof(string)) ||
                        sourceProperty.PropertyType.IsArray ||
                        sourceProperty.PropertyType.Equals(typeof(Guid)) ||
                        sourceProperty.PropertyType.Equals(typeof(DateTime)) ||
                        Nullable.GetUnderlyingType(sourceProperty.PropertyType) != null
                       )
                    {
                        if (sourceProperty.PropertyType != targetProperty.PropertyType || !targetProperty.CanWrite)
                        {
                            break;
                        }
                        targetProperty.SetValue(target, sourceProperty.GetValue(source));
                    }
                    else
                    {
                        var sourceValue = sourceProperty.GetValue(source);
                        if (sourceValue == null)
                        {
                            break;
                        }

                        var targetValue = Map(sourceValue, targetProperty.PropertyType);
                        if (targetValue == null)
                        {
                            break;
                        }
                        targetProperty.SetValue(target, targetValue);
                    }
                    break;
                }
            }
            return target;
        }

    }
}
