using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StateSharp.Generator.Builders;
using System;
using System.Linq;
using System.Text;

namespace StateSharp.Generator
{
    [Generator]
    public class StateGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var builder = new StringBuilder();

            foreach (var tree in context.Compilation.SyntaxTrees)
            {
                foreach (var @class in tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where(x => x.AttributeLists.Any(y => y.ToString().Equals("[StateObject]"))))
                {
                    StateFileBuilder.Build(context, GetNamespace(@class), @class);
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }

        private string GetNamespace(ClassDeclarationSyntax classDeclarationSyntax)
        {
            var node = classDeclarationSyntax.Parent;
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
