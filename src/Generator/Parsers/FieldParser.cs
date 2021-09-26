using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using StateSharp.Generator.Models;

namespace StateSharp.Generator.Parsers
{
    public static class FieldParser
    {
        public static List<FieldModel> Parse(ClassDeclarationSyntax @class)
        {
            var fields = new List<FieldModel>();
            foreach (var member in @class.Members.OfType<PropertyDeclarationSyntax>().Where(x => x.AttributeLists.Any(y => y.ToString().Equals("[StateProperty]"))))
            {
                fields.Add(Parse(member));
            }
            return fields;
        }

        public static FieldModel Parse(PropertyDeclarationSyntax member)
        {
            var name = member.Identifier.ToString();
            var type = TypeParser.Parse(member.Type);
            return new FieldModel(name, type);
        }
    }
}
