using System.Collections.Generic;

namespace StateSharp.Core.Collections
{
    internal class PathTreeNode<T>
    {
        private readonly List<T> _values;
        private readonly Dictionary<string, PathTreeNode<T>> _children;

        public IReadOnlyList<T> Values => _values.AsReadOnly();
        public IReadOnlyDictionary<string, PathTreeNode<T>> Children => _children;

        internal PathTreeNode()
        {
            _values = new List<T>();
            _children = new Dictionary<string, PathTreeNode<T>>();
        }

        public PathTreeNode<T> AddChild(string key)
        {
            var child = new PathTreeNode<T>();
            _children.Add(key, child);
            return child;
        }

        public void AddValue(T value)
        {
            _values.Add(value);
        }

        public void RemoveValue(T value)
        {
            if (_values.Remove(value) == false)
            {
                throw new KeyNotFoundException();
            }
        }
    }
}