using Microsoft.CodeAnalysis;

namespace StateSharp.Generator
{
    [Generator]
    public class MyGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var source = "class Foo { }";

            if (source != null)
            {
                context.AddSource("generated.cs", source);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
