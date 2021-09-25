using System.Collections.Generic;

namespace StateSharp.Generator.Models
{
    public class StructModel
    {
        public string Name { get; }
        public string Namespace { get; }
        public List<FieldModel> Fields { get; }

        public StructModel(string name, string @namespace, List<FieldModel> fields)
        {
            Name = name;
            Namespace = @namespace;
            Fields = fields;
        }
    }
}
