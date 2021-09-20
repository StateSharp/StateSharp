namespace StateSharp.Generator.Models
{
    public class FieldModel
    {
        public string Name { get; }
        public TypeModel Type { get; }

        public FieldModel(string name, TypeModel type)
        {
            Name = name;
            Type = type;
        }
    }
}
