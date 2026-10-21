using UnityEngine;
using UnityEngine.Events;

public abstract class ParamEventListener<T,U> : MonoBehaviour
{   
    public ParamEvent<T,U> _event;
    public UnityEvent<T,U> _response;

    private void OnEnable()
    {
        _event.Subscribe(this);
    }

    private void OnDisable()
    {
        _event.Unsubscribe(this);
    }

    public void OnEventInvoked(T param1, U param2)
    {
        _response.Invoke(param1, param2);
    } 
}
