using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource[] hitSFX;
    public AudioSource[] missSFX;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI noteCounterText;

    private static int comboScore;
    private static int totalNotes;
    private static int totalScore;
    public static int missedNotes;
    public static int hitedNotes;
    public static int okNotes;
    public static int goodNotes;
    public static int greatNotes;
    public static int excellentNotes;

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
        okNotes = 0;
        goodNotes = 0;
        greatNotes = 0;
        excellentNotes = 0;
        Lane.ResetTotalTimeStamps();
        UpdateTotalNotes();
        UpdateTotalScore();
    }

    public static void Hit(int baseScore, string feedbackMessage)
    {
        comboScore += 1;
        totalScore += baseScore;
        totalNotes--;
        hitedNotes++;
        UpdateNoteCounter();
        UpdateTotalScore();
        PlayRandomSound(Instance.hitSFX);
        CountFeedback(feedbackMessage);
        CheckEndGame();
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
        CheckEndGame();
    }

    public static void UpdateTotalNotes()
    {
        totalNotes = Lane.totalTimeStamps;
        UpdateNoteCounter();
    }

    private static void UpdateNoteCounter()
    {
        Instance.noteCounterText.text = $"Left Notes \n{totalNotes.ToString()}";

    }

    private static void UpdateTotalScore()
    {
        Instance.totalScoreText.text = $"Total Score \n{totalNotes.ToString()}";

    }

    private static void PlayRandomSound(AudioSource[] audioSources)
    {
        if (audioSources.Length == 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, audioSources.Length);
        audioSources[randomIndex].Play();
    }

    private static void CheckEndGame()
    {
        if (totalNotes <= 0)
        {
            Instance.StartCoroutine(EndGame());
        }
    }
    private static void CountFeedback(string feedbackMessage)
    {
        switch (feedbackMessage)
        {
            case "Excellent":
                excellentNotes++;
                break;
            case "Great":
                greatNotes++;
                break;
            case "Good":
                goodNotes++;
                break;
            case "Ok":
                okNotes++;
                break;
        }
    }
    private static IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);

        GameData.ComboScore = comboScore;
        GameData.TotalScore = totalScore;
        GameData.MissedNotes = missedNotes;
        GameData.HitedNotes = hitedNotes;
        GameData.NextScene = "ResultsScreen";
        GameData.OkNotes = okNotes;
        GameData.GoodNotes = goodNotes;
        GameData.GreatNotes = greatNotes;
        GameData.ExcellentNotes = excellentNotes;

        SceneManager.LoadScene(GameData.NextScene);
    }
}
