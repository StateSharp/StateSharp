using Microsoft.CodeAnalysis.CSharp.Syntax;
using StateSharp.Generator.Models;

namespace StateSharp.Generator.Parsers
{
    public static class TypeParser
    {
        public static TypeModel Parse(TypeSyntax type)
        {
            return new TypeModel(string.Empty, string.Empty);
        }
    }
}