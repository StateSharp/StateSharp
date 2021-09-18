using System.Collections.Generic;

namespace StateSharp.Generator.Models
{
    public class ClassModel
    {
        public string Name { get; }
        public string Namespace { get; }
        public Dictionary<string, string> Fields { get; }

        public ClassModel(string name, string @namespace, Dictionary<string, string> fields)
        {
            Name = name;
            Namespace = @namespace;
            Fields = fields;
        }
    }
}
