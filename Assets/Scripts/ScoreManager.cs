using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource[] hitSFX;
    public AudioSource[] missSFX;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI noteCounterText;

    public string Username;
    private static int comboScore;
    private static int totalNotes;
    private static int totalScore;
    public static int missedNotes;
    public static int hitedNotes;

    public static int ComboScore
    {
        get { return comboScore; }
        private set { comboScore = value; }
    }

    public static int TotalScore
    {
        get { return totalScore; }
        private set { totalScore = value; }
    }

    void Start()
    {
        Instance = this;
        comboScore = 0;
        totalScore = 0;
        missedNotes = 0;
        hitedNotes = 0;
        Lane.ResetTotalTimeStamps();
        UpdateTotalNotes();
        UpdateTotalScore();
    }

    public static void Hit(int baseScore, int precisionBonus)
    {
        comboScore += 1;
        int comboBonus = comboScore;
        int scoreToAdd = baseScore + precisionBonus + comboBonus;
        totalScore += scoreToAdd;
        totalNotes--;
        hitedNotes++;
        UpdateNoteCounter();
        UpdateTotalScore();
        PlayRandomSound(Instance.hitSFX);
    }

    public static void Miss()
    {
        comboScore = 0;
        totalScore -= 75;
        totalNotes--;
        missedNotes++;
        UpdateNoteCounter();
        UpdateTotalScore();
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

    private static void UpdateTotalScore()
    {
        Instance.totalScoreText.text = "Total Score: " + totalScore.ToString();
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
