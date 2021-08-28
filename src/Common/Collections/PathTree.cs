using StateSharp.Core.Services;
using System.Collections.Generic;

namespace StateSharp.Core.Collections
{
    internal class PathTree<T>
    {
        private readonly PathTreeNode<T> _root;

        internal PathTree()
        {
            _root = new PathTreeNode<T>();
        }

        public void Add(string path, T value)
        {
            var node = _root;
            foreach (var key in PathService.SplitPath(path))
            {
                if (node.Children.TryGetValue(key, out var child))
                {
                    node = child;
                }
                else
                {
                    node = node.AddChild(key);
                }
            }
            node.AddValue(value);
        }

        public void Remove(string path, T value)
        {
            var node = _root;
            foreach (var key in PathService.SplitPath(path))
            {
                node = node.Children.TryGetValue(key, out var child) ? child : throw new KeyNotFoundException();
            }
            node.RemoveValue(value);
        }

        public List<T> GetMatches(string path)
        {
            var node = _root;
            var result = new List<T>();
            foreach (var key in PathService.SplitPath(path))
            {
                result.AddRange(node.Values);
                if (node.Children.TryGetValue(key, out var child))
                {
                    node = child;
                }
                else
                {
                    return result;
                }
            }
            result.AddRange(node.Values);

            var queue = new Queue<PathTreeNode<T>>(node.Children.Values);
            while (queue.TryDequeue(out var next))
            {
                result.AddRange(next.Values);
                foreach (var child in next.Children.Values)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }
    }
}