using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script attached to Options Menu game object and manages all things related to it.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    // Variables for visibility, modification or references in Inspector

    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    // Awake

    void Awake()
    {
        SoundManager.Instance.ChangeVolume("Master", masterVolumeSlider.value);
        SoundManager.Instance.ChangeVolume("Music", musicVolumeSlider.value);
        SoundManager.Instance.ChangeVolume("SFX", sfxVolumeSlider.value);
    }

    // Start

    void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeVolume("Master", value));
        musicVolumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeVolume("Music", value));
        sfxVolumeSlider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeVolume("SFX", value));
    }
}
