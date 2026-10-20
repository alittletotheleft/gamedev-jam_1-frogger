using System.Collections.Generic;
using UnityEngine;

public abstract class ParamEvent<T> : ScriptableObject
{   
    private List<ParamEventListener<T>> _listeners = new List<ParamEventListener<T>>();
    
    public void Invoke(T param)
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnEventInvoked(param);
        }
    }

    public void Subscribe(ParamEventListener<T> listener)
    {
        _listeners.Add(listener);
    }

    public void Unsubscribe(ParamEventListener<T> listener)
    {
        _listeners.Remove(listener);
    }
}
