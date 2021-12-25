using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameEventTrigger : MonoBehaviour 
{
    public List<EventData> events;

    void Awake()
    {
        foreach (var ev in events)
        {
            ev.gameEvent.RegisterListener(ev);
        }
    }

    private void OnDestroy()
    {
        foreach (var ev in events)
        {
            ev.gameEvent.UnregisterListener(ev);
        }
    }
}

[Serializable]
public class EventData
{
    public string name;
    public GameEvent gameEvent;
    public EventCallback response;
    
    /// <summary>
    /// Calls when Event is Raised with given parameters
    /// Call all responses register in the Unity Event
    /// </summary>
    /// <param name="obj">parameters</param>
    public void OnEventRaised(params object[] obj)
    {
        response.Invoke(obj);
    }
}

[Serializable]
public class EventCallback : UnityEvent<object[]>
{ }