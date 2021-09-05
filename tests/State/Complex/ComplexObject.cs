using StateSharp.Core.States;

namespace State.Complex
{
    public class ComplexObject
    {
        public IStateString String { get; private set; }
        public IStateObject<ComplexObject> Object { get; private set; }
        public IStateStructure<ComplexStructure> Structure { get; private set; }
        public IStateDictionary<IStateString> StringDictionary { get; private set; }
        public IStateDictionary<IStateStructure<int>> PrimativeDictionary { get; private set; }
        public IStateDictionary<IStateStructure<ComplexStructure>> StructureDictionary { get; private set; }
        public IStateDictionary<IStateObject<ComplexObject>> ObjectDictionary { get; private set; }
    }
}
