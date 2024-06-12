using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource[] hitSFX;  
    public AudioSource[] missSFX;
    public TextMeshProUGUI scoreText;
    static int comboScore;

    void Start()
    {
        Instance = this;
        comboScore = 0;
    }

    public static void Hit()
    {
        comboScore += 1;
        PlayRandomSound(Instance.hitSFX);
    }

    public static void Miss()
    {
        comboScore = 0;
        PlayRandomSound(Instance.missSFX);
    }

    private void Update()
    {
        scoreText.text = comboScore.ToString();
    }

    private static void PlayRandomSound(AudioSource[] audioSources)
    {
        if (audioSources.Length == 0)
            return;

        int randomIndex = Random.Range(0, audioSources.Length);
        audioSources[randomIndex].Play();
    }
}
