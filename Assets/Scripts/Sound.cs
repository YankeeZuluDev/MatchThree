using UnityEngine;

/// <summary>
/// This is a container for audio clip and audio id pair 
/// </summary>

[System.Serializable]
public struct Sound
{
    public AudioID audioID;
    public AudioClip audioClip;
}
