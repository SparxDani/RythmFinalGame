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
    public TextMeshProUGUI noteCounterText;

    static int comboScore;

    private static int totalNotes;

    void Start()
    {
        Instance = this;
        comboScore = 0;
        UpdateTotalNotes();
    }

    public static void Hit()
    {
        comboScore += 1;
        totalNotes--;
        UpdateNoteCounter();
        PlayRandomSound(Instance.hitSFX);
    }

    public static void Miss()
    {
        comboScore = 0;
        totalNotes--;
        UpdateNoteCounter();
        PlayRandomSound(Instance.missSFX);
    }

    public static void UpdateTotalNotes()
    {
        totalNotes = Lane.totalTimeStamps;
        UpdateNoteCounter();
    }

    private void Update()
    {
        scoreText.text = "Combo Score: " + comboScore.ToString();
    }

    private static void UpdateNoteCounter()
    {
        Instance.noteCounterText.text = "Remaining Notes: " + totalNotes.ToString();
    }

    private static void PlayRandomSound(AudioSource[] audioSources)
    {
        if (audioSources.Length == 0)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, audioSources.Length);
        audioSources[randomIndex].Play();
    }
}
