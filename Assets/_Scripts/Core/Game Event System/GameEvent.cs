using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener eventListener)
    {
        if (!eventListeners.Contains(eventListener))
        {
            eventListeners.Add(eventListener);
        }
    }

    public void UnRegisterListener(GameEventListener eventListener)
    {
        if (eventListeners.Contains(eventListener))
        {
            eventListeners.Remove(eventListener);
        }
    }
}