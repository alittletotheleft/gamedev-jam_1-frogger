using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{   
    public Event _event;
    public UnityEvent _response;

    private void OnEnable()
    {
        _event.Subscribe(this);
    }

    private void OnDisable()
    {
        _event.Unsubscribe(this);
    }

    public void OnEventInvoked()
    {
        _response.Invoke();
    }
}
