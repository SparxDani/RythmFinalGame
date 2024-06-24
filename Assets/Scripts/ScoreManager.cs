using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource[] hitSFX;
    public AudioSource[] missSFX;
    public TextMeshProUGUI TotalScore;
    public TextMeshProUGUI noteCounterText;

    private static int comboScore;
    private static int totalNotes;
    private static int totalScore;

    public static int ComboScore
    {
        get { return comboScore; }
        private set { comboScore = value; }
    }

    void Start()
    {
        Instance = this;
        comboScore = 0;
        Lane.ResetTotalTimeStamps();
        UpdateTotalNotes();
    }

    public static void Hit()
    {
        comboScore += 1;
        totalNotes --;
        UpdateNoteCounter();
        PlayRandomSound(Instance.hitSFX);
    }

    public static void Miss()
    {
        comboScore = 0;
        totalNotes --;
        UpdateNoteCounter();
        PlayRandomSound(Instance.missSFX);
    }

    public static void UpdateTotalNotes()
    {
        totalNotes = Lane.totalTimeStamps;
        UpdateNoteCounter();
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
