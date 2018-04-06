using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Analyzer1
{
    public static class NamedTypeSymbolExtensions
    {
        public static bool IsTypeOrInheritsFrom(this INamedTypeSymbol namedTypeSymbol, string typeToMatch)
        {
            if (namedTypeSymbol.TypeKind == TypeKind.Error)
            {
                return false;
            }

            if (namedTypeSymbol.SpecialType == SpecialType.System_Object)
            {
                return false;
            }

            var typeString = namedTypeSymbol.ToDisplayString();
            if (typeString == typeToMatch)
            {
                return true;
            }

            return IsTypeOrInheritsFrom(namedTypeSymbol.BaseType, typeToMatch);
        }

        public static IEnumerable<AttributeData> SafeAttributes(this INamedTypeSymbol methodSymbol)
        {
            return methodSymbol.GetAttributes().Where(data => data.AttributeClass.TypeKind != TypeKind.Error);
        }
    }
}