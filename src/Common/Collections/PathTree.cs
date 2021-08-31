using System;
using System.Collections.Generic;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Services;

namespace StateSharp.Core.Collections
{
    internal class PathTree<T>
    {
        private readonly PathTreeNode _root;

        internal PathTree()
        {
            _root = new PathTreeNode();
        }

        public void Add(string path, Action<IStateEvent> value)
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

        public void Remove(string path, Action<IStateEvent> value)
        {
            var node = _root;
            foreach (var key in PathService.SplitPath(path))
            {
                node = node.Children.TryGetValue(key, out var child)
                    ? child
                    : throw new SubscriptionNotFoundException(path);
            }

            node.RemoveValue(value);
        }

        public List<Action<IStateEvent>> GetMatches(string path)
        {
            var node = _root;
            var result = new List<Action<IStateEvent>>();
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

            var queue = new Queue<PathTreeNode>(node.Children.Values);
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