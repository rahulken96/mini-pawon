using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PEMAIN_PREFS_VOLUME_MUSIC = "VolumeMusic";

    private AudioSource audioSource;
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        
        volume = PlayerPrefs.GetFloat(PEMAIN_PREFS_VOLUME_MUSIC, .3f);
        audioSource.volume = volume;
    }

    public void UbahVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }

        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PEMAIN_PREFS_VOLUME_MUSIC, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
