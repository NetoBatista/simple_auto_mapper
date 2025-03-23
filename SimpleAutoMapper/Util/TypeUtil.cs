using SimpleAutoMapper.TypeDefinition;

namespace SimpleAutoMapper.Util
{
    internal static class TypeUtil
    {
        internal static bool IsPrimitiveOrSimpleType(Type type)
        {
            return type.IsEnum ||
                   type.IsPrimitive ||
                   SimpleTypes.Types.Contains(type) ||
                   Nullable.GetUnderlyingType(type) != null;
        }
    }
}
