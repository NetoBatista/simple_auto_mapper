using SimpleAutoMapper.Mappers;
using System.Collections;

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
                ObjectMapper.Map(source, destination);
                return destination;
            }

            var sourceList = (IEnumerable)source;
            var itemType = typeof(TDestination).GetGenericArguments()[0];
            var destinationList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType))!;

            foreach (var item in sourceList)
            {
                var mappedItem = CollectionItemMapper.Map(itemType, item);
                destinationList.Add(mappedItem);
            }

            return (TDestination)destinationList;
        }
    }
}