﻿using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace StateSharp.Core.Collections
{
    internal class PathTreeNode
    {
        private readonly List<Action<IStateEvent>> _values;
        private readonly Dictionary<string, PathTreeNode> _children;

        public IReadOnlyList<Action<IStateEvent>> Values => _values.AsReadOnly();
        public IReadOnlyDictionary<string, PathTreeNode> Children => _children;

        internal PathTreeNode()
        {
            _values = new List<Action<IStateEvent>>();
            _children = new Dictionary<string, PathTreeNode>();
        }

        public PathTreeNode AddChild(string key)
        {
            var child = new PathTreeNode();
            _children.Add(key, child);
            return child;
        }

        public void AddValue(Action<IStateEvent> value)
        {
            _values.Add(value);
        }

        public void RemoveValue(Action<IStateEvent> value)
        {
            if (_values.Remove(value) == false)
            {
                throw new SubscriptionNotFoundException($"No subscription found for {value}");
            }
        }
    }
}