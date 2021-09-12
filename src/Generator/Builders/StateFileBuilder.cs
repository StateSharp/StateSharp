using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Text;

namespace StateSharp.Generator.Builders
{
    public static class StateFileBuilder
    {
        public static void Build(GeneratorExecutionContext context, string @namespace, ClassDeclarationSyntax @class)
        {
            var source = new StringBuilder();

            source.AppendLine($"namespace {@namespace}");
            source.AppendLine("{");

            source.AppendLine($"    public class {@class.Identifier}State : I{@class.Identifier}State");
            source.AppendLine("    {");

            foreach (var property in @class.Members.OfType<PropertyDeclarationSyntax>().Where(x => x.AttributeLists.Any(y => y.ToString().Equals("[StateProperty]"))))
            {
                var type = property.Type.ToString();
                var name = property.Identifier.ToString();
                source.AppendLine($"        private {type} _{name};");
                source.AppendLine($"        public {type} {name} {{ get; set; }}");
            }

            source.AppendLine("    }");

            source.AppendLine();

            source.AppendLine($"    public interface I{@class.Identifier}State : I{@class.Identifier}StateReadOnly");
            source.AppendLine("    {");

            foreach (var property in @class.Members.OfType<PropertyDeclarationSyntax>().Where(x => x.AttributeLists.Any(y => y.ToString().Equals("[StateProperty]"))))
            {
                var type = property.Type.ToString();
                var name = property.Identifier.ToString();
                source.AppendLine($"        public new {type} {name} {{ get; set; }}");
            }

            source.AppendLine("    }");

            source.AppendLine();

            source.AppendLine($"    public interface I{@class.Identifier}StateReadOnly");
            source.AppendLine("    {");

            foreach (var property in @class.Members.OfType<PropertyDeclarationSyntax>().Where(x => x.AttributeLists.Any(y => y.ToString().Equals("[StateProperty]"))))
            {
                var type = property.Type.ToString();
                var name = property.Identifier.ToString();
                source.AppendLine($"        public {type} {name} {{ get; }}");
            }

            source.AppendLine("    }");

            source.AppendLine("}");

            context.AddSource($"{@class.Identifier}State.cs", source.ToString());
        }
    }
}
