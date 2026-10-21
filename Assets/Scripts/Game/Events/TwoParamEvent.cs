using System.Collections.Generic;
using UnityEngine;

public abstract class ParamEvent<T,U> : ScriptableObject
{   
    private List<ParamEventListener<T,U>> _listeners = new List<ParamEventListener<T,U>>();
    
    public void Invoke(T param1, U param2)
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnEventInvoked(param1, param2);
        }
    }

    public void Subscribe(ParamEventListener<T,U> listener)
    {
        _listeners.Add(listener);
    }

    public void Unsubscribe(ParamEventListener<T,U> listener)
    {
        _listeners.Remove(listener);
    }
}
