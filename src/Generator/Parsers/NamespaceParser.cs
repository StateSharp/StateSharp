using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace StateSharp.Generator.Parsers
{
    public static class NamespaceParser
    {
        public static string Parse(ClassDeclarationSyntax @class)
        {
            var node = @class.Parent;
            while (node is not NamespaceDeclarationSyntax)
            {
                node = node?.Parent;
                if (node == null)
                {
                    throw new NullReferenceException();
                }
            }
            return ((NamespaceDeclarationSyntax)node).Name.ToFullString().TrimEnd();
        }
    }
}
