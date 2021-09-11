using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Text;

namespace StateSharp.Generator
{
    [Generator]
    public class StateInterfaceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var builder = new StringBuilder();

            foreach (var tree in context.Compilation.SyntaxTrees)
            {
                foreach (var stateClass in tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where(x => x.AttributeLists.Any(y => y.ToFullString().Contains("[State]"))))
                {
                    var ns = GetNamespace(stateClass);
                    builder.AppendLine(ns);
                }
            }

            context.AddSource("generated.cs", builder.ToString());
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
            return ((NamespaceDeclarationSyntax)node).Name.ToFullString();
        }
    }
}
