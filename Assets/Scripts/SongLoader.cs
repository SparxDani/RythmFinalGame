using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongLoader : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        LoadSong(GameData.AudioClip);
    }

    public void LoadSong(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
        }
        else
        {
            Debug.LogError("AudioClip is not set in GameData.");
        }
    }
}