using Notteam.World;
using System;
using System.Collections.Generic;

public class EventDispatcherSystem : WorldSystem
{
    private Dictionary<Type, List<Delegate>> _events = new Dictionary<Type, List<Delegate>>();

    public void SubscribeToEvent<T>(Action<T> action)
    {
        if (!_events.ContainsKey(typeof(T)))
            _events[typeof(T)] = new List<Delegate>();

        _events[typeof(T)].Add(action);
    }

    public void UnSubscribeFromEvent<T>(Action<T> action)
    {
        if (_events.ContainsKey(typeof(T)))
            _events[typeof(T)].Remove(action);
    }

    public void InvokeEvent<T>(T @event)
    {
        if (_events.ContainsKey(typeof(T)))
        {
            foreach (var action in _events[typeof(T)])
            {
                action.DynamicInvoke(@event);
            }
        }
    }
}
