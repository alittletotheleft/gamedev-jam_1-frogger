using UnityEngine;
using UnityEngine.Events;

public abstract class ParamEventListener<T> : MonoBehaviour
{   
    public ParamEvent<T> _event;
    public UnityEvent<T> _response;

    private void OnEnable()
    {
        _event.Subscribe(this);
    }

    private void OnDisable()
    {
        _event.Unsubscribe(this);
    }

    public void OnEventInvoked(T param)
    {
        _response.Invoke(param);
    }
}
