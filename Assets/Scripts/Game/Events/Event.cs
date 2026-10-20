using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Events/Event")]
public class Event : ScriptableObject
{   
    private List<EventListener> _listeners = new List<EventListener>();
    
    public void Raise()
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnEventInvoked();
        }
    }

    public void Subscribe(EventListener listener)
    {
        _listeners.Add(listener);
    }

    public void Unsubscribe(EventListener listener)
    {
        _listeners.Remove(listener);
    }
}
