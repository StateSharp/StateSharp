using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StateSharp.Generator.Models;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Generator.Parsers
{
    public static class ClassParser
    {
        public static List<ClassModel> Parse(GeneratorExecutionContext context)
        {
            var models = new List<ClassModel>();
            foreach (var tree in context.Compilation.SyntaxTrees)
            {
                foreach (var @class in tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where(x => x.AttributeLists.Any(y => y.ToString().Equals("[StateObject]"))))
                {
                    models.Add(new ClassModel($"{@class.Identifier.Text}State", NamespaceParser.Parse(@class), FieldsParser.Parse(@class)));
                }
            }
            return models;
        }
    }
}
