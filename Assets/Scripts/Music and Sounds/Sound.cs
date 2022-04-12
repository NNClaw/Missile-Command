using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class with default variables for each declared sound in game. Declared in Inspector in Sound Manager game object.
/// </summary>
[System.Serializable]
public class Sound
{
    // Variables for tweaking in Inspector

    [Tooltip("The name of the song")]
    [SerializeField] string soundName;

    [Tooltip("The tag of game object, where the source will be attached to")]
    [SerializeField] string soundTag;

    [Tooltip("Reference for the audio clip")]
    [SerializeField] AudioClip clip;

    [Tooltip("Starting volume of the audio clip")]
    [Range(0f, 1f)]
    [SerializeField] float volume;

    [Tooltip("Starting pitch of the audio clip")]
    [Range(0.1f, 3f)]
    [SerializeField] float pitch;

    [Tooltip("Is the audio clip going to be looped?")]
    [SerializeField] bool loop;

    // Internal class references

    AudioSource source;

    // Properties

    public string SoundName { get { return soundName; } }
    public string SoundTag { get { return soundTag; } }
    public AudioClip Clip { get { return clip; } }
    public float Volume { get { return volume; } set { volume = value; } }
    public float Pitch { get { return pitch; } set { pitch = value; } }
    public AudioSource Source { get { return source; } set { source = value; } }
    public bool Loop { get { return loop; } }
}
