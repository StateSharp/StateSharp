using System.Collections.Generic;

namespace StateSharp.Generator.Models
{
    public class ClassModel
    {
        public string Name { get; }
        public string Namespace { get; }
        public List<FieldModel> Fields { get; }

        public ClassModel(string name, string @namespace, List<FieldModel> fields)
        {
            Name = name;
            Namespace = @namespace;
            Fields = fields;
        }
    }
}
