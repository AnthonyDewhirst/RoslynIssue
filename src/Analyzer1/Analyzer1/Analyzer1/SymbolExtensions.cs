using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Analyzer1
{
    public static class SymbolExtensions
    {
        public static bool HasAttributes(this ISymbol methodSymbol, string baseOrType)
        {
            return methodSymbol.GetAttributes(baseOrType).Any();
        }

        public static IEnumerable<AttributeData> GetAttributes(this ISymbol symbol, string baseOrType)
        {
            return symbol.SafeAttributes().Where(o => o.AttributeClass.OriginalDefinition.IsTypeOrInheritsFrom(baseOrType));
        }

        public static IEnumerable<AttributeData> SafeAttributes(this ISymbol symbol)
        {
            return symbol.GetAttributes().Where(data => data.AttributeClass.TypeKind != TypeKind.Error);
        }
    }
}