using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for playing music and SFX
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private List<Sound> sounds;

    private Dictionary<AudioID, AudioClip> soundsDictionary;

    private void Awake()
    {
        soundsDictionary = new(sounds.Count);

        foreach (Sound sound in sounds)
        {
            soundsDictionary.Add(sound.audioID, sound.audioClip);
        }
    }

    private void Start() => PlayMusic();

    private void PlayMusic()
    {
        musicSource.clip = soundsDictionary[AudioID.Music];
        musicSource.Play();
    }

    public void PlaySFX(AudioID audioID)
    {
        if (soundsDictionary.ContainsKey(audioID))
            sFXSource.PlayOneShot(soundsDictionary[audioID]);
        else
            throw new KeyNotFoundException($"No clip with {audioID} ID was found in dictionary");
    }
}
