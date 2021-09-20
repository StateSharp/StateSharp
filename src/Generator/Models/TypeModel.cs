namespace StateSharp.Generator.Models
{
    public class TypeModel
    {
        public string Name { get; }
        public string Namespace { get; }

        public TypeModel(string name, string @namespace)
        {
            Name = name;
            Namespace = @namespace;
        }
    }
}
