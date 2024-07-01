using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicBG : MonoBehaviour
{
    private static MusicBG instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex > 1)
        {
            Destroy(gameObject);
        }
    }

    public void LoadMusicSelectorScene()
    {
        SceneManager.LoadScene("MusicSelector");
    }
}