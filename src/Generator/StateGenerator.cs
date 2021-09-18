using Microsoft.CodeAnalysis;
using StateSharp.Generator.Parsers;

namespace StateSharp.Generator
{
    [Generator]
    public class StateGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var classModels = ClassParser.Parse(context);
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
