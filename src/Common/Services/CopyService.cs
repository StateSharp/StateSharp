using StateSharp.Core.Events;
using StateSharp.Core.States;
using System;

namespace StateSharp.Core.Services
{
    internal static class CopyService
    {
        internal static T CopyState<T>(T obj, IStateEventManager eventManager)
        {
            return (T)CopyState(typeof(T), obj, eventManager);
        }

        internal static object CopyState(Type type, object obj, IStateEventManager eventManager)
        {
            var result = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
            {
                var value = (IStateBase)property.GetValue(obj) ?? throw new NullReferenceException();
                property.SetValue(result, value.Copy(eventManager));
            }
            return result;
        }
    }
}