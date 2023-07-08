using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for event scriptable object
/// </summary>

[CreateAssetMenu(menuName ="Game Event")]

public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new();

    public void Raise()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnRaise();
        }
    }

    public void Subscribe(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnSubscribe(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
