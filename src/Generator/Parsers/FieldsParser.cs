using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Generator.Parsers
{
    public static class FieldsParser
    {
        public static Dictionary<string, string> Parse(ClassDeclarationSyntax @class)
        {
            foreach (var member in @class.Members.Where(x => x.AttributeLists.Any(y => y.ToString().Equals("[StateProperty]"))))
            {

            }
            return new Dictionary<string, string>();
        }
    }
}
