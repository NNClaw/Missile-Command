using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// The script, which is attached to Sound Manager game object and manages all the audio operations. Also, this game object persists on all of the scenes since we don't want the audio to vanish.
/// </summary>
public class SoundManager : MonoBehaviour
{
    // Variables for declaring, visibility or tweaking in the Inspector
    [SerializeField] Sound[] sounds;

    // Internal class references

    public static SoundManager Instance;

    // Internal struct references

    bool exists;

    // Awake

    void Awake()
    {
        #region Singleton and Boolean modify

        if (Instance == null)
        {
            Instance = this;
            exists = false;
        }
        else
        {
            exists = true;
            Destroy(gameObject);
        }
            
        #endregion

        DontDestroyOnLoad(gameObject);

        // For each declared sound in Inspector...
        foreach(Sound s in sounds)
        {
            try
            {
                // ...if the sound has tag of BGM, add AudioSource component to the game object with that tag
                if (s.SoundTag == "BGM" && !exists)
                {
                    s.Source = GameObject.FindGameObjectWithTag("BGM").AddComponent<AudioSource>();
                }

                //...if the sound has tag of SFX, add AudioSource component to the game object with that tag
                else if (s.SoundTag == "SFX" && !exists)
                {
                    s.Source = GameObject.FindGameObjectWithTag("SFX").AddComponent<AudioSource>();
                }

                s.Source.volume = s.Volume;
                s.Source.pitch = s.Pitch;
                s.Source.clip = s.Clip;
                s.Source.loop = s.Loop;
            }
            catch
            {
                Debug.LogWarning("The audio source already exists");
            }
        }
    }

    // Start

    void Start()
    {
        PlaySound("Theme");    
    }

    /// <summary>
    /// This method plays the sound with the given name.
    /// </summary>
    /// <param name="nameOfSound"></param>
    public void PlaySound(string nameOfSound)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == nameOfSound);

        if (s == null)
        {
            Debug.LogWarning("Sound with name " + nameOfSound + " is not found!");
            return;
        }

        s.Source.Play();
    }

    /// <summary>
    /// This method plays the sound with given name and tag. Just in case.
    /// </summary>
    /// <param name="nameOfSound"></param>
    /// <param name="tagOfSound"></param>
    public void PlaySound(string nameOfSound, string tagOfSound)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == nameOfSound && sound.SoundTag == tagOfSound);

        if(s == null)
        {
            Debug.LogWarning("Sound with name " + nameOfSound + " is not found!");
            return;
        }

        s.Source.Play();
    }

    /// <summary>
    /// This method changes volume of given audio channel. Used primarily for volume sliders in Options menu.
    /// </summary>
    /// <param name="audioChannel"> Name of audio channel. </param>
    /// <param name="value"> The volume of audio channel. </param>
    public void ChangeVolume(string audioChannel, float value)
    {
        string newString = audioChannel.ToLower();
        Sound s;

        switch (newString)
        {
            case "master":
                AudioListener.volume = value;
                break;
            case "music":
                s = Array.Find(sounds, sound => sound.SoundTag == "BGM");
                s.Source.volume = value;
                break;
            case "sfx":
                s = Array.Find(sounds, sound => sound.SoundTag == "SFX");
                s.Source.volume = value;
                break;
            default:
                break;
        }
    }

}
