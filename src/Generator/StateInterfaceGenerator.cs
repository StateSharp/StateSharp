using Microsoft.CodeAnalysis;
using System.Text;

namespace StateSharp.Generator
{
    [Generator]
    public class StateInterfaceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var builder = new StringBuilder();

            context.AddSource("generated.cs", builder.ToString());
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
