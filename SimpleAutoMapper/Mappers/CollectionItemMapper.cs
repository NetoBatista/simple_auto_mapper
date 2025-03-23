using SimpleAutoMapper.Util;

namespace SimpleAutoMapper.Mappers
{
    internal static class CollectionItemMapper
    {
        internal static object? Map(Type destinationType, object source)
        {
            if (source == null)
            {
                return null;
            }

            if (TypeUtil.IsPrimitiveOrSimpleType(destinationType) || destinationType.IsEnum)
            {
                return source;
            }

            var destination = Activator.CreateInstance(destinationType);
            if (destination != null)
            {
                ObjectMapper.Map(source, destination);
            }

            return destination;
        }
    }
}
